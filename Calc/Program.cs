using System;
using System.Diagnostics;
using System.IO;

namespace DayzdaAlegria {
    class Program {
        static void Main(string[] args) {
            string fileconfig = "Config.txt";
            if (File.Exists(fileconfig))
            {   
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Foi encontrado um arquivo de configuração salvo deseja usalo? (y ou n)");
                char select1 = char.Parse(Console.ReadLine());
                if (select1 == 'y')
                {
                    string nick1, mods1, ip1, porta1, exe1;
                    (nick1, mods1, ip1, porta1, exe1) = Carregar(fileconfig);
                    Abrirdayz(nick1, mods1, ip1, porta1, exe1);
                }
                else
                {
                    Obterdados();
                }
            }
            else{

                Obterdados();
            }
            static void Obterdados() {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Insira o ip do servidor:");
                string ip = Console.ReadLine();
                Console.WriteLine("Insira a porta do servidor:");
                string porta = Console.ReadLine();
                Console.WriteLine("Insira o seu nickname");
                string nick = Console.ReadLine();
                Console.WriteLine("Insira o diretorio da pasta mods:");
                string pastamods = Console.ReadLine();
                string mods = Obtermods(pastamods);
                Console.WriteLine(mods);
                Console.WriteLine("Insira o caminho do executavel:");
                string exe = Console.ReadLine();
                Console.WriteLine("Deseja Salvar esses dados para acessar na proxima execussão? (y ou n)");
                char select = char.Parse(Console.ReadLine());
                if (select == 'y')
                {
                    Salvar(nick, mods, ip, porta, exe);
                }
                Abrirdayz(nick, mods, ip, porta, exe);
            }

            static string Obtermods(string pastamods) {
                string resultado = " ";
                if (Directory.Exists(pastamods))
                {
                    string[] pastas = Directory.GetDirectories(pastamods);
                    resultado = "Mods\\" + string.Join(";Mods\\", Array.ConvertAll(pastas, Path.GetFileName));
                    return resultado;
                }
                else
                {
                    Console.WriteLine("Diretorio nao encontrado");
                }
                return resultado;
            }
            static void Abrirdayz(string nick, string mods, string ip, string porta, string exe) {
                if (File.Exists(exe))
                {
                    string argumentos;
                    argumentos = String.Format(" -name={0} \"-mod={1}\" -connect={2} -port={3} -noFilePatching ", nick, mods, ip , porta);
                    Process.Start(exe, argumentos);
                    Console.WriteLine("Dayz iniciado com sucesso");
                }
                else
                {
                    Console.WriteLine("Erro ao abrir o game");
                }
            }
            static void Salvar(string nick, string mods, string ip, string porta, string exe) {
                string[] linhas = { nick, mods, ip, porta, exe};
                string nome = "Config.txt";
                File.WriteAllLines(nome, linhas);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Configuracões salvas com sucesso");
            }
            static (string, string, string, string, string) Carregar(string fileconfig) {
                if (File.Exists(fileconfig)) { }
                string[] linhas = File.ReadAllLines(fileconfig);
                return (linhas[0], linhas[1], linhas[2], linhas[3], linhas[4]);
            }
        }
    }
}