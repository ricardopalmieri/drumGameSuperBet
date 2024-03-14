using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class CSVReader : MonoBehaviour
{
    [System.Serializable]
    public class PrizeData
    {
        public int id;
        public string premio;
        public int inserido;
        public int consumido;
        public int total;

        public PrizeData(int _id, string _premio, int _inserido, int _consumido, int _total)
        {
            id = _id;
            premio = _premio;
            inserido = _inserido;
            consumido = _consumido;
            total = _total;
        }
    }

    // Variável para armazenar o arquivo CSV
    public TextAsset csvFile;

    // Lista para armazenar os dados do CSV
    private List<PrizeData> csvData = new List<PrizeData>();

    // Variável para armazenar a quantidade de prêmios disponíveis
    private int availablePrizes;

    public int sorteado;

    public bool esgotou;

    // Caminho do CSV
    static string csvPath = Application.dataPath + "/CSV/premiosTable.csv";

    // Método para ler o arquivo CSV

    private string ReadCSVFile()
    {
        try
        {
            // Check if the file exists
            if (File.Exists(csvPath))
            {
                // Read the entire file content
                return File.ReadAllText(csvPath);
            }
            else
            {
                Debug.LogError("File not found at: " + csvPath);
                return null;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error reading file: " + ex.Message);
            return null;
        }
    }

    // Método para ler o arquivo CSV
    private void ReadCSV()
    {
        string csvText = ReadCSVFile();

        if (csvText == null)
        {
            Debug.LogError("Arquivo CSV não foi atribuído.");
            return;
        }

        StringReader reader = new StringReader(csvText);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            string[] rowData = line.Split(';');
            if (rowData.Length != 5)
            {
                Debug.LogError("Formato de linha inválido: " + line);
                continue;
            }

            int id;
            if (!int.TryParse(rowData[0], out id))
            {
                Debug.LogError("ID inválido: " + rowData[0]);
                continue;
            }

            string premio = rowData[1];

            int inserido;
            if (!int.TryParse(rowData[2], out inserido))
            {
                Debug.LogError("Valor 'inserido' inválido: " + rowData[2]);
                continue;
            }

            int consumido;
            if (!int.TryParse(rowData[3], out consumido))
            {
                Debug.LogError("Valor 'consumido' inválido: " + rowData[3]);
                continue;
            }

            int total;
            if (!int.TryParse(rowData[4], out total))
            {
                Debug.LogError("Valor 'total' inválido: " + rowData[4]);
                continue;
            }

            csvData.Add(new PrizeData(id, premio, inserido, consumido, total));
        }

        reader.Close();

        // Definindo a quantidade de prêmios disponíveis
        availablePrizes = csvData.Count;

        // Debug.Log do conteúdo do CSV
        DebugCSV();
    }


    // Método para imprimir o conteúdo do CSV
    private void DebugCSV()
    {
        foreach (PrizeData prize in csvData)
        {
            string line = string.Format("ID: {0}, Prêmio: {1}, Inserido: {2}, Consumido: {3}, Total: {4}",
                                        prize.id, prize.premio, prize.inserido, prize.consumido, prize.total);
            Debug.Log(line);
        }
    }


    // Método para sortear um prêmio disponível
    public int DrawPrize()
    {
        // Se não houver prêmios disponíveis, retorna -1
        if (availablePrizes <= 0 || csvData.Count == 0)
        {
            Debug.Log("Não há prêmios disponíveis para sorteio.");
            return -1;
        }

        // Criando uma lista de índices de prêmios disponíveis
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < csvData.Count; i++)
        {
            if (csvData[i].total > 0)
            {
                availableIndexes.Add(i);
            }
        }

        // Verificando se há prêmios disponíveis para sorteio
        if (availableIndexes.Count == 0)
        {
            Debug.LogError("Todos os prêmios estão esgotados.");
            esgotou = true;
            return -1;
        }

        // Sorteando um número entre os índices dos prêmios disponíveis
        int randomNumber = UnityEngine.Random.Range(0, availableIndexes.Count);
        int selectedPrizeIndex = availableIndexes[randomNumber];

        // Atualizando as quantidades utilizada e disponível no CSV
        csvData[selectedPrizeIndex].consumido++;
        csvData[selectedPrizeIndex].total--;

        // Reduzindo a contagem de prêmios disponíveis
        availablePrizes--;

        // Salvando as alterações no CSV
        //SaveCSV();

        sorteado = csvData[selectedPrizeIndex].id;
        Debug.Log("n.sort: " + sorteado);

        // Retornando o ID do prêmio sorteado
        return csvData[selectedPrizeIndex].id;
    }



    // Método para salvar as alterações no arquivo CSV
    public void SaveCSV()
    {
        //
        //string filePath = Path.Combine(Application.dataPath, "Resources/" + csvFile.name + ".csv");
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            foreach (PrizeData prize in csvData)
            {
                string line = string.Format("{0};{1};{2};{3};{4}",
                                            prize.id, prize.premio, prize.inserido, prize.consumido, prize.total);
                writer.WriteLine(line);
            }
        }

        Debug.Log("Alterações salvas com sucesso.");
    }

    public void SaveCSV_GPT()
    {
        try
        {
            string content = "";

            foreach (PrizeData prize in csvData)
            {
                string line = string.Format("{0};{1};{2};{3};{4}",
                                            prize.id, prize.premio, prize.inserido, prize.consumido, prize.total);

                string.Concat(content, line, "\n");
            }


            File.WriteAllText(csvPath, content);
            Debug.Log(content);
            Debug.Log("Alterações salvas com sucesso.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }

    }

    private void Awake()
    {
        esgotou = false;
        ReadCSV();
        //StartCoroutine(espera());
    }

    private IEnumerator espera()
    {        
        yield return new WaitForSeconds(2.0f);
        DrawPrize();
    }
}
