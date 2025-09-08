using System;

public class ContaFinanceira
{
    // --------- Campos estáticos (ID auto-incremental) ---------
    private static int _nextId = 1;
    private const int CAPACIDADE_EXTRATO = 64;

    // --------- Propriedades ----------
    public int IdConta { get; }
    public string NomeTitular { get; private set; }
    public decimal Saldo { get; private set; }
    public bool Ativa { get; private set; }
    public DateTime DataCriacao { get; }

    // --------- Histórico (array interno) ---------
    private readonly string[] Movimentacoes = new string[CAPACIDADE_EXTRATO];
    private int _movCount = 0;

    // --------- Construtor ----------
    public ContaFinanceira(string nomeTitular) : this(nomeTitular, 0m) { }

    public ContaFinanceira(string nomeTitular, decimal saldoInicial)
    {
        IdConta = _nextId++;
        if (string.IsNullOrWhiteSpace(nomeTitular) || nomeTitular.Trim().Length < 3)
            throw new ArgumentException("Nome do titular inválido.", nameof(nomeTitular));

        NomeTitular = nomeTitular.Trim();
        if (saldoInicial < 0)
            throw new ArgumentOutOfRangeException(nameof(saldoInicial), "Saldo inicial não pode ser negativo.");

        Saldo = saldoInicial;
        Ativa = true;
        DataCriacao = DateTime.UtcNow;

        Registrar($"[{UtcNowStr()}] ABERTURA: conta criada com saldo {Saldo:C}");
    }

    // --------- Operações ----------
    public void Depositar(decimal valor)
    {
        AssegurarAtiva();
        AssegurarValorPositivo(valor);

        Saldo += valor;
        Registrar($"[{UtcNowStr()}] DEPÓSITO: +{valor:C} | Saldo: {Saldo:C}");
    }

    public void Sacar(decimal valor)
    {
        AssegurarAtiva();
        AssegurarValorPositivo(valor);

        if (Saldo < valor)
            throw new InvalidOperationException("Saldo insuficiente.");

        Saldo -= valor;
        Registrar($"[{UtcNowStr()}] SAQUE: -{valor:C} | Saldo: {Saldo:C}");
    }

    public void TransferirParaId(int contaDestinoId, decimal valor)
    {
        AssegurarAtiva();
        AssegurarValorPositivo(valor);
        if (Saldo < valor) throw new InvalidOperationException("Saldo insuficiente.");

        Saldo -= valor;
        Registrar($"[{UtcNowStr()}] TRANSFERÊNCIA (pendente): -{valor:C} para Conta #{contaDestinoId} | Saldo: {Saldo:C}");
    }

    public decimal ConsultarSaldo() => Saldo;

    public void Bloquear()
    {
        if (!Ativa) return;
        Ativa = false;
        Registrar($"[{UtcNowStr()}] BLOQUEIO: conta bloqueada.");
    }

    public void Desbloquear()
    {
        if (Ativa) return;
        Ativa = true;
        Registrar($"[{UtcNowStr()}] DESBLOQUEIO: conta ativa.");
    }

    public string[] GerarExtrato()
    {
        var copia = new string[_movCount];
        Array.Copy(Movimentacoes, copia, _movCount);
        return copia;
    }

    // --------- Helpers privados ----------
    private void AssegurarAtiva()
    {
        if (!Ativa)
            throw new InvalidOperationException("Conta bloqueada. Operação não permitida.");
    }

    private static void AssegurarValorPositivo(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "Valor deve ser positivo.");
    }

    private void Registrar(string descricao)
    {
        if (_movCount < CAPACIDADE_EXTRATO)
        {
            Movimentacoes[_movCount++] = descricao;
        }
        else
        {
            // política simples de "buffer deslizante": descarta o mais antigo
            Array.Copy(Movimentacoes, 1, Movimentacoes, 0, CAPACIDADE_EXTRATO - 1);
            Movimentacoes[CAPACIDADE_EXTRATO - 1] = descricao;
        }
    }

    private static string UtcNowStr() => DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss 'UTC'");
}