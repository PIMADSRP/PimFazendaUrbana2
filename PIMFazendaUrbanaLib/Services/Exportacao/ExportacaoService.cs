using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace PIMFazendaUrbanaLib
{
    public class ExportacaoService : IExportacaoService
    {
        public byte[] Exportar(IEnumerable<object> dados, string formato)
        {
            Console.WriteLine($"Dados recebidos: {JsonConvert.SerializeObject(dados)}");
            switch (formato.ToLower())
            {
                case "xlsx":
                    return GerarExcel(dados);

                case "csv":
                    return GerarCsv(dados);

                default:
                    throw new ArgumentException("Formato não suportado.");
            }
        }

        private byte[] GerarExcel<T>(IEnumerable<T> dados)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Exportação");

            // Obter propriedades do tipo T
            var propriedades = typeof(T).GetProperties();

            // Adicionar cabeçalhos
            for (int i = 0; i < propriedades.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = propriedades[i].Name; // Cabeçalhos
            }

            // Adicionar dados
            int row = 2;
            foreach (var item in dados)
            {
                for (int i = 0; i < propriedades.Length; i++)
                {
                    // Converte o valor para string ou outro tipo adequado
                    var valor = propriedades[i].GetValue(item)?.ToString() ?? string.Empty;
                    worksheet.Cell(row, i + 1).Value = valor;
                }
                row++;
            }

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        private byte[] GerarCsv<T>(IEnumerable<T> dados)
        {
            var propriedades = typeof(T).GetProperties();
            var sb = new StringBuilder();

            // Adiciona cabeçalhos
            sb.AppendLine(string.Join(",", propriedades.Select(p => p.Name)));

            // Adiciona dados
            foreach (var item in dados)
            {
                sb.AppendLine(string.Join(",", propriedades.Select(p => p.GetValue(item)?.ToString())));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

    }


}