using System.ComponentModel;
using System.Diagnostics;
using Python.Runtime;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using record_of_clicks_on_files;

namespace rainflow
{
    public partial class Form1 : Form
    {
        public string conteudoArquivo = "";
        List<KeyValuePair<double, double>> resultado = new List<KeyValuePair<double, double>>();
        DataTable data = new DataTable();

        public Form1()
        {
            InitializeComponent();

            AllowDrop = true;
            listBox1.AllowDrop = true;

            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
            button2.Enabled = false;
            Runtime.PythonDLL = @"C:\Python312\python312.dll";
            PythonEngine.Initialize();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void openFileDialog1_FileOk_1(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            listBox1.Items.Clear();
            button2.Enabled = true;

            foreach (string file in files)
            {
                try
                {
                    string content = File.ReadAllText(file);
                    conteudoArquivo = content;
                    listBox1.Items.Add(Path.GetFileName(file));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao ler o arquivo {Path.GetFileName(file)}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<double, double>> dados = limparArquivo(conteudoArquivo);
            resultado = encontrarPontos(dados);

            List<Tempos> tempos = resultado.Select(x => new Tempos(x.Key, x.Value)).ToList();

            var bindingList = new BindingList<Tempos>(tempos);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            criarColunaComBotaoNoDataGrid();

            button1.Enabled = false;
            button3.Show();
        }

        private List<KeyValuePair<double, double>> encontrarPontos(List<KeyValuePair<double, double>> resultado)
        {
            List<KeyValuePair<double, double>> resulta = new List<KeyValuePair<double, double>>();

            for (int i = 0; i < resultado.Count - 1; i++)
            {
                if (i != 0)
                {
                    double valorAtual = resultado[i].Value;
                    double valorAnterior = resultado[i - 1].Value;
                    double valorSeguinte = resultado[i + 1].Value;

                    if (valorAtual < valorAnterior && valorAtual < valorSeguinte)
                    {
                        resulta.Add(new KeyValuePair<double, double>(resultado[i].Key, resultado[i].Value));
                    }
                    else if (valorAtual > valorAnterior && valorAtual > valorSeguinte)
                    {
                        resulta.Add(new KeyValuePair<double, double>(resultado[i].Key, resultado[i].Value));
                    }
                }
                else
                {
                    resulta.Add(new KeyValuePair<double, double>(resultado[i].Key, resultado[i].Value));
                }
            }

            return resulta;
        }

        private void criarColunaComBotaoNoDataGrid()
        {
            var indice = dataGridView1.Rows.Count - 1;

            DataGridViewButtonColumn colunaBotaoExcluir = new DataGridViewButtonColumn();
            colunaBotaoExcluir.HeaderText = "";
            colunaBotaoExcluir.Text = "Excluir";
            colunaBotaoExcluir.UseColumnTextForButtonValue = true;
            colunaBotaoExcluir.Width = 50;
            dataGridView1.Columns.Add(colunaBotaoExcluir);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifique se o clique ocorreu na coluna de botão e na linha desejada
            if (e.RowIndex >= 0 && e.ColumnIndex == 2 && dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Excluir")
            {
                var index = resultado.Find(f => f.Key.ToString().Equals(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                resultado.Remove(index);
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private List<KeyValuePair<double, double>> limparArquivo(string conteudo)
        {

            List<KeyValuePair<double, double>> lista = new List<KeyValuePair<double, double>>();

            var vetor = conteudo.Replace(" ", "");
            vetor = vetor.Replace("E+000", "E+000;").Replace("Time(sec),Value", "").Replace("\r\n", ";\r\n");
            var algo = vetor.Split(";");

            for (int i = 0; i < algo.Length; i++)
            {

                if ((i >= 0) && (i <= 2))
                {

                }
                else
                {
                    try
                    {
                        lista.Add(new KeyValuePair<double, double>(double.Parse(algo[i]), double.Parse(algo[i + 1])));
                        i++;
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return lista;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultado.Clear();
            button1.Enabled = true;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            button2.Enabled = false;
            button3.Enabled = true;
            button3.Hide();
            button4.Hide();
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            List<double> valores = resultado.Select(x => x.Value).ToList();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            List<Ciclos> ciclos = new List<Ciclos>();
            using (Py.GIL())
            {
                dynamic rainflowModule = Py.Import("rainflow");
                dynamic result = rainflowModule.extract_cycles(valores);
                foreach (var resulta in result)
                {
                    ciclos.Add(
                        new Ciclos(
                            double.Parse(resulta[0].ToString()),
                            double.Parse(resulta[1].ToString()),
                            double.Parse(resulta[2].ToString()),
                            double.Parse(resulta[3].ToString()) + 1,
                            double.Parse(resulta[4].ToString()) + 1)
                        );
                }
            }

            var bindingList = new BindingList<Ciclos>(ciclos);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            button4.Show();
            button3.Enabled = false;
        }

        private void GerarExcel()
        {
            try { 
            data = converterResultsInDataTable();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(data, "Rainflow");
                wb.SaveAs(Path.Combine(path, "Rainflow.xlsx"));
            }

            MessageBox.Show($"Excel gerado -> {Path.Combine(path, "Rainflow.xlsx")}", "Excel gerado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erro ao gerar excel, verifique se já está aberto!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable converterResultsInDataTable()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            return dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GerarExcel();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Certifique-se de que o usuário realmente deseja fechar a aplicação
                DialogResult result = MessageBox.Show("Deseja realmente sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Encerre a aplicação
                    PythonEngine.Shutdown();
                    Application.Exit();
                }
                else
                {
                    // Cancela o fechamento do formulário
                    e.Cancel = true;
                }
            }
        }

    }
}

public class Tempos
{
    public double Tempo { get; set; }
    public double Valor { get; set; }

    public Tempos(double tempo, double valor)
    {
        Tempo = tempo;
        Valor = valor;
    }
}

public class Ciclos
{
    public double Range {  get; set; }
    public double Mean { get; set; }
    public double Count {  get; set; }
    public double StartIndex { get; set; }
    public double EndIndex { get; set; }

    public Ciclos(double range, double mean, double count, double startIndex, double endIndex)
    {
        Range = range;
        Mean = mean;
        Count = count;
        StartIndex = startIndex;
        EndIndex = endIndex;
    }
}