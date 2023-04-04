using System.Runtime.Intrinsics.X86;
using System;
using System.Collections;
using System.Reflection;

namespace GestaoDeEquipamentos
{
    internal class Program
    {
        static ArrayList listaEquipamentos = new ArrayList();
        static ArrayList listaChamados = new ArrayList();
        static int contador = 1;

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                MostrarMenu();

            } while (true);


        }

        static ArrayList BuscarEquipamento(string idEquipamento)
        {

            foreach (ArrayList item in listaEquipamentos)
            {
                if (item[0].ToString() == idEquipamento) {
                    return item;
                }
            }
            return null;
        }

        static ArrayList FormularEquipamento()
        {
            Console.Write("\nNome do Equipamento: ");
            string nomeEquipamento = Console.ReadLine();

            while (nomeEquipamento.Length < 6)
            {
                Console.Clear();
                Console.Write("\nDigite Novamente o Nome do Equipamento: ");
                nomeEquipamento = Console.ReadLine();
            }

            Console.Write("\nPreço aquisição: ");
            string precoAquisicao = Console.ReadLine();

            Console.Write("\nNumero de Série: ");
            string numeroDeSerie = Console.ReadLine();

            Console.Write("\nData de fabricação: ");
            string dataDeFabricacao = Console.ReadLine();

            Console.Write("\nFabricante: ");
            string fabricante = Console.ReadLine();
            Console.Clear();
           return new ArrayList() { nomeEquipamento, precoAquisicao, numeroDeSerie, dataDeFabricacao, fabricante };
        }

        static void Cadastrar()
        {
            Console.Clear();
            string id = contador++.ToString();
            ArrayList dadosFormulario = FormularEquipamento();
            ArrayList equipamento = new ArrayList() {id, dadosFormulario[0], dadosFormulario[1], dadosFormulario[2], dadosFormulario[3], dadosFormulario[4] };
            listaEquipamentos.Add(equipamento);
        }


        static void MostrarMenu()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(" CONTROLE E GESTÃO DE EQUIPAMENTOS  ");
            Console.WriteLine("------------------------------------");
            
            Console.WriteLine("Escolhe a opção no menu:");
            Console.WriteLine("Opção [1] Gerenciar Equipamentos");
            Console.WriteLine("Opção [2] Gerenciar Chamados");
            Console.WriteLine("Opção [3] para sair ");
            int opcao = Convert.ToInt32(Console.ReadLine());
            
            switch(opcao)
            {
                case 1: MenuEqp(); break;
                case 2: MenuChamados(); break;
                case 3: Sair(); break;
            }
        }

        static void MenuEqp()
        {
            Console.Clear();
            Console.WriteLine("Equipamentos");
            Console.WriteLine("Digite [1] para Cadastrar");
            Console.WriteLine("Digite [2] para Visualizar");
            Console.WriteLine("Digite [3] para Atualizar");
            Console.WriteLine("Digite [4] para Excluir");
            int opcao = Convert.ToInt32(Console.ReadLine());

            switch(opcao)
            {
                case 1: Cadastrar(); break;
                case 2: MostrarLista(); Console.ReadLine(); break;
                case 3: EditarEqp(); break;
                case 4: ExcluirEqp(); break;
            }
            Console.ReadLine();
        }

        static void MostrarLista()
        {
            Console.Clear();
            Console.WriteLine("{0,-5}| {1,-30}| {2,-15}| {3,-20}| {4,-15}| {5,-15}",
                              "ID", "Nome", "Equipamento", "Preço de Aquisição", "Número de Série", "Data de Fabricação");
            foreach (ArrayList item in listaEquipamentos)
            {
                Console.WriteLine("{0,-5}| {1,-30}| {2,-15}| {3,-20:C}| {4,-15}| {5,-15:d}",
                                  item[0], item[1], item[2], item[3], item[4], item[5]);
            }
        }

        static void EditarEqp()
        {
            Console.Clear();
            Console.WriteLine("Digite o ID do equipamento que deseja editar: ");
            string idEquipamento = Console.ReadLine();

            ArrayList equipamentoEncontrado = BuscarEquipamento(idEquipamento);

            ArrayList formulario = FormularEquipamento();

            int idNaLista = listaEquipamentos.IndexOf(equipamentoEncontrado);
            ArrayList equipamentoAtualizado = new ArrayList() { idEquipamento, formulario[0], formulario[1], formulario[2], formulario[3], formulario[4] };
            listaEquipamentos[idNaLista] = equipamentoAtualizado;
        }

        static void ExcluirEqp()
        {
            Console.Clear();
            Console.Write("Digite o ID do equipamento que deseja excluir: ");
            string idEquipamento = Console.ReadLine();

            ArrayList equipamentoEncontrado = BuscarEquipamento(idEquipamento);

            int idNaLista = listaEquipamentos.IndexOf(equipamentoEncontrado);
            if (equipamentoEncontrado == null)
            {
                Console.WriteLine("Equipamento não encontrado.");
            }
            else
            {
                listaEquipamentos.Remove(equipamentoEncontrado);
                Console.WriteLine("Equipamento excluído com sucesso.");
            }
            Console.ReadLine();
        }

        static void Sair()
        {
            Console.WriteLine("Saindo...");
            Environment.Exit(0);
        }

        static void MenuChamados()
        {
            Console.Clear();
            Console.WriteLine("Chamados");
            Console.WriteLine("Digite [1] para Cadastrar");
            Console.WriteLine("Digite [2] para Visualizar");
            Console.WriteLine("Digite [3] para Atualizar");
            Console.WriteLine("Digite [4] para Excluir");
            int opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1: CadastrarChamados(); break;
                case 2: MostrarChamados(); break;
                case 3: AtualizarChamado(); break;
                case 4: ExcluirChamado(); break;
            }
            Console.ReadLine();
        }

        static ArrayList BuscarChamado(string idChamados)
        {
            foreach (ArrayList item in listaChamados)
            {
                if (item[0].ToString() == idChamados)
                {
                    return item;
                }
            }
            return null;
        } 

        static ArrayList FormularChamado()
        {
            Console.Write("\nTítulo: ");
            string titulo = Console.ReadLine();

            Console.Write("\nDescrição do Chamado: ");
            string descricao = Console.ReadLine();

            Console.Write("\nNome do Equipamento: ");
            string equipamento = Console.ReadLine();

            Console.Write("\nData de Abertura: ");
            string dataDeAbertura = Console.ReadLine();

            Console.Clear();
            return new ArrayList() { titulo, descricao, equipamento, dataDeAbertura };
        }

        static void CadastrarChamados()
        {
            Console.Clear();
            MostrarLista();
            string id = contador++.ToString();
            Console.Write("\nDigite o ID do equipamento que você deseja abrir chamado: ");
            string idEquipamento = Console.ReadLine();
            ArrayList equipamento = BuscarEquipamento(idEquipamento);

            ArrayList dadosFormulario = FormularChamado();
            ArrayList chamado = new ArrayList() { id, equipamento[0], dadosFormulario[0], dadosFormulario[1], dadosFormulario[2], dadosFormulario[3]};
            listaChamados.Add(chamado);
        }

        static void MostrarChamados()
        {
            Console.Clear();
            Console.WriteLine("{0,-2} {1,-20} {2,-20} {3,-20} {4,-15}", "ID", "Título", "Equipamento", "Data de Abertura", "Dias em Aberto");

            foreach (ArrayList item in listaChamados)
            {
                ArrayList equipamento = BuscarEquipamento(item[1].ToString());
                DateTime dataConvertida = Convert.ToDateTime(item[5]);
                int diasEmAberto = Convert.ToInt32(DateTime.Now.Subtract(dataConvertida).TotalDays);
                Console.WriteLine($"{item[0],-2} {item[2],-20} {equipamento[1],-20} {item[5],-20} {diasEmAberto} dias");
            }
            Console.ReadLine();
        }

        static void AtualizarChamado()
        {
            Console.Clear();
            Console.WriteLine("Digite o ID do chamado que deseja atualizar: ");
            string idChamados = Console.ReadLine();

            ArrayList chamadoEncontrado = BuscarChamado(idChamados);

            if (chamadoEncontrado == null)
            {
                Console.WriteLine("Chamado não encontrado.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine($"ID: {chamadoEncontrado[0]}");
                Console.WriteLine($"Título: {chamadoEncontrado[2]}");
                Console.WriteLine($"Equipamento: {chamadoEncontrado[1]}");
                Console.WriteLine($"Data de Abertura: {chamadoEncontrado[5]}");
                Console.WriteLine($"Descrição: {chamadoEncontrado[3]}");
                Console.WriteLine($"Status: {chamadoEncontrado[4]}");

                Console.WriteLine("\nDigite o novo título do chamado:");
                chamadoEncontrado[2] = Console.ReadLine();

                Console.WriteLine("Digite a nova descrição do chamado:");
                chamadoEncontrado[3] = Console.ReadLine();

                Console.WriteLine("Digite o novo status do chamado:");
                chamadoEncontrado[4] = Console.ReadLine();

                Console.WriteLine("Chamado atualizado com sucesso!");
            }
            Console.ReadLine();
        }

        static void ExcluirChamado()
        {
            Console.Clear();
            Console.Write("Digite o ID do chaamdo que deseja excluir: ");
            string idChamados = Console.ReadLine();

            ArrayList chamadoEncontrado = BuscarChamado(idChamados);

            int idNaLista = listaChamados.IndexOf(chamadoEncontrado);
            if (chamadoEncontrado == null)
            {
                Console.WriteLine("Chamado não encontrado.");
                Console.ReadLine();
                return;
            }
            else
            {
                listaChamados.Remove(chamadoEncontrado);
                Console.WriteLine("Chamado excluído com sucesso!");
            }
            Console.ReadLine();
        }
    }
}