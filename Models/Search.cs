
namespace PoliziaMunicipaleMulte.Models
{ 
public class VerbaliTrascritti
{
    public int IDAnagrafica { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public int TotaleVerbaliTrascritti { get; set; }
}

public class VerbaleDettaglio
{
    public int IDAnagrafica { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public decimal Importo { get; set; }
    public DateTime DataViolazione { get; set; }
    public int DecurtamentoPunti { get; set; }
}

public class TotalePerTrasgressore
{
    public int IDAnagrafica { get; set; }
    public int TotaleVerbali { get; set; }
}
}