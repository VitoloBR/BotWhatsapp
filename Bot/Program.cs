using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Bot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://web.whatsapp.com";
            List<string> contatos = new List<string>();
            int opcao = 0;
            int quanti = 0;
            int quantidade = 0;
            int mensa = 0;
            int quantiConta = 0;
            string mensagem = "Vazio";
            bool logica = false;

            while (logica == false) { 
            
                Console.WriteLine("=============================");
                Console.WriteLine("             MENU            ");
                Console.WriteLine("=============================");
                Console.WriteLine("[1] Visualizar contatos");
                Console.WriteLine("[2] Adicionar contatos");
                Console.WriteLine("[3] Remover contatos");
                Console.WriteLine("[4] Visualizar mensagem");
                Console.WriteLine("[5] Adicionar/substituir mensagem");
                Console.WriteLine("[6] Enviar mensagem");
                Console.WriteLine("[7] Sair");
                Console.WriteLine("=============================");
                bool resultado = false;
                while (resultado != true)
                {
                    Console.Write("Qual menu gostaria? ");

                    string opcaos = (Console.ReadLine());
                    bool result = int.TryParse(opcaos, out opcao); 

                    if (result == true)
                    {
                        resultado = true;
                    }
                    Console.Clear();
                }

                switch (opcao)
                {
                    case 1:
                        if (quantiConta > 0)
                        {
                            foreach (string contato in contatos)
                            {
                                Console.WriteLine(contato);
                            }
                        }

                        else
                        {
                            Console.WriteLine("Não há contato salvo");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Digite o nome do contato que deseja adicionar: ");
                        contatos.Add(Console.ReadLine());
                        quanti++;
                        quantiConta++;
                        break;

                    case 3:
                        Console.WriteLine("Digite o nome do contato que deseja remover: ");
                        contatos.Remove(Console.ReadLine());

                        if (quantiConta != contatos.Count())
                        {
                            quanti--;
                            quantiConta--;
                        }

                        else
                        {
                            Console.WriteLine("Não há contato com esse nome, digite corretamente!");
                        }

                        break;

                    case 4:
                        Console.WriteLine(mensagem);
                        break;

                    case 5:
                        Console.WriteLine("Qual mensagem deseja enviar: ");
                        mensagem = Console.ReadLine();
                        mensa++;
                        break;

                    case 6:
                        if (quanti < 1 && mensa < 1)
                        {
                            Console.WriteLine("Adicione um contato e uma mensagem antes");
                            
                        }

                        else if(mensa < 1)
                        {
                            Console.WriteLine("Adicione uma mensagem antes");
                        }

                        else if (quanti < 1)
                        {
                            Console.WriteLine("Adicione um contato antes!");
                        }

                        else
                        {
                            resultado = false;
                            while (resultado != true)
                            {
                                Console.WriteLine("Quantas vezes deseja enviar a mensagem: " + mensagem);

                                string opcaos = (Console.ReadLine());
                                bool result = int.TryParse(opcaos, out opcao); 

                                if (result == true)
                                {
                                    resultado = true;
                                }
                                Console.Clear();
                            }
                            
                            ChromeDriver driver = new ChromeDriver();
                            driver.Navigate().GoToUrl(url);
                            driver.Manage().Window.Maximize();

                            Thread.Sleep(45000);

                            foreach (var contato in contatos)
                            {
                                //< div data - testid = "chat-list-search" title = "Caixa de texto de pesquisa" role = "textbox" class="_13NKt copyable-text selectable-text" contenteditable="true" data-tab="3" dir="ltr"></div>
                                var caixaTexto = driver.FindElement(OpenQA.Selenium.By.XPath("//*[@id='side']/div[1]/div/div/div[2]/div/div[2]"));
                                if (caixaTexto is null) return;

                                caixaTexto.SendKeys(contato);

                                Thread.Sleep(1000);

                                //<span dir="auto" title="Sra Encrenca" class="ggj6brxn gfz4du6o r7fjleex g0rxnol2 lhj4utae le5p0ye3 l7jjieqr i0jNr"><span class="matched-text i0jNr">Sra Encrenca</span></span>
                                var conversa = driver.FindElement(OpenQA.Selenium.By.XPath($"//span[@title='{contato}']"));
                                if (conversa is null) return;
                                conversa.Click();

                                Thread.Sleep(1000);

                                for (int i = 0; i < opcao; i++)
                                {
                                    //<div class="fd365im1 to2l77zo bbv8nyr4 mwp4sxku gfz4du6o ag5g9lrv" contenteditable="true" role="textbox" spellcheck="true" title="Mensagem" data-testid="conversation-compose-box-input" data-tab="10" data-lexical-editor="true" style="user-select: text; white-space: pre-wrap; word-break: break-word;"><p class="selectable-text copyable-text"><br></p></div>
                                    var conversaTexto = driver.FindElement(OpenQA.Selenium.By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]"));
                                    if (conversaTexto is null) return;

                                    conversaTexto.SendKeys(mensagem);

                                    Thread.Sleep(500);

                                    //<span data-testid="send" data-icon="send" class=""><svg viewBox="0 0 24 24" width="24" height="24" class=""><path fill="currentColor" d="M1.101 21.757 23.8 12.028 1.101 2.3l.011 7.912 13.623 1.816-13.623 1.817-.011 7.912z"></path></svg></span>

                                    var botao = driver.FindElement(OpenQA.Selenium.By.XPath($"//span[@data-testid='send']"));
                                    if (botao is null) return;
                                    botao.Click();

                                    Thread.Sleep(500);


                                }


                            }
                            Console.Clear();
                        }
                        break;
                    case 7:
                        logica = true;
                        break;

                    default:
                        Console.WriteLine("Erro, escolha uma opção disponivel!");
                        break;

                }
            }

            Console.WriteLine("Obrigado por utilizar o robo!!");
            Console.WriteLine("Criado por Vinicius Vitolo");
        }
    }
}