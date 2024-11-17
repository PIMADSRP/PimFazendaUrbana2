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

            if (!dados.Any())
            {
                throw new ArgumentException("Nenhum dado fornecido.");
            }

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

        private byte[] GerarExcel(IEnumerable<object> dados)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Exportação");

            // Pega as propriedades do primeiro objeto
            var propriedades = dados.First().GetType().GetProperties();

            // Adiciona e estiliza cabeçalhos
            for (int i = 0; i < propriedades.Length; i++)
            {
                var headerCell = worksheet.Cell(1, i + 1);
                headerCell.Value = propriedades[i].Name;
                headerCell.Style.Font.Bold = true;
                headerCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#23992c");
                headerCell.Style.Font.FontColor = XLColor.White;
                headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Adiciona dados e bordas
            int row = 2;
            foreach (var item in dados)
            {
                for (int i = 0; i < propriedades.Length; i++)
                {
                    var valor = propriedades[i].GetValue(item)?.ToString() ?? string.Empty;
                    worksheet.Cell(row, i + 1).Value = valor;
                }
                row++;
            }

            // Ajusta largura das colunas
            worksheet.Columns().AdjustToContents();

            // Adiciona bordas ao redor da tabela
            var range = worksheet.Range(1, 1, row - 1, propriedades.Length);
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Congela o cabeçalho
            worksheet.SheetView.FreezeRows(1);

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }


        private byte[] GerarCsv(IEnumerable<object> dados)
        {
            var sb = new StringBuilder();

            // Pega as propriedades do primeiro objeto
            var propriedades = dados.First().GetType().GetProperties();

            // Adiciona cabeçalhos
            sb.AppendLine(string.Join(",", propriedades.Select(p => p.Name)));

            // Adiciona os dados
            foreach (var item in dados)
            {
                sb.AppendLine(string.Join(",", propriedades.Select(p => p.GetValue(item)?.ToString())));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }



}