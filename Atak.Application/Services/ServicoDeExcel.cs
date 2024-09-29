using OfficeOpenXml;
using Atak.Core.Entities;

namespace Atak.Application.Services
{
    public class ServicoDeExcel
    {
        public byte[] GerarExcel(List<Cliente> clientes)
        {
            using (var pacote = new ExcelPackage())
            {
                var planilha = pacote.Workbook.Worksheets.Add("Clientes");

              
                planilha.Cells[1, 1].Value = "Nome";
                planilha.Cells[1, 2].Value = "Email";
                planilha.Cells[1, 3].Value = "Telefone";
                planilha.Cells[1, 4].Value = "Data de Nascimento";


                for (int i = 0; i < clientes.Count; i++)
                {
                    planilha.Cells[i + 2, 1].Value = clientes[i].Nome;
                    planilha.Cells[i + 2, 2].Value = clientes[i].Email;
                    planilha.Cells[i + 2, 3].Value = clientes[i].Telefone;
                    planilha.Cells[i + 2, 4].Value = clientes[i].DataNascimento.ToShortDateString();
                }

                return pacote.GetAsByteArray();
            }
        }
    }
}
