using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System;

namespace Agenda
{
    class Program
    {
        //static ConsoleColor colormenu = ConsoleColor.Green;
        //static ConsoleColor colorcase = ConsoleColor.Cyan;
        static void Main(string[] args)
        {
            // Console.ForegroundColor = colormenu;
            char opcio = '0';
            CrearFitxer();
            while (opcio != 'q' && opcio != 'Q')
            {
                do
                {
                    Console.Clear();
                    PintarAgenda(Agenda());
                    opcio = Console.ReadKey().KeyChar;
                }
                while (!ValidarOpcio(opcio));
                Console.Clear();
                SeleccionarOpcio(opcio);
            }
        }

        // Mètode Agenda
        static string Agenda()
        {
            string agenda =
                $"\n╔════════════════════════════════╗\n" +
               $"║             AGENDA             ║\n" +
               $"╠════════════════════════════════╣\n" +
               $"║  1 - Donar d'alta              ║\n" +
               $"║  2 - Recuperar Usuari          ║\n" +
               $"║  3 - Modificar Usuari          ║\n" +
               $"║  4 - Eliminar Usuari           ║\n" +
               $"║  5 - Mostrar Agenda            ║\n" +
               $"║  6 - Ordenar Agenda            ║\n" +
               $"║  q - Sortir                    ║\n" +
               $"╚════════════════════════════════╝";
            return agenda;
        }

        // Mètode ValidarOpcio
        static bool ValidarOpcio(char opcio)
        {
            bool valid = false;
            if (opcio > '0' && opcio < '7' || opcio == 'q' || opcio == 'Q')
            {
                valid = true;
            }
            return valid;
        }

        // Mètode Validar Si o No
        static bool SiONo(ref char opcio)
        {
            bool valid = false;
            if (opcio == 'S') opcio = 's';
            if (opcio == 'N') opcio = 'n';

            if (opcio == 's' || opcio == 'n')
            {
                valid = true;
            }
            return valid;
        }

        // Mètode SeleccionarOpcio
        static void SeleccionarOpcio(char opcio)
        {
            PintarOpcio(Agenda(), opcio);
            switch (opcio)
            {
                case '1':
                    DonarAlta();
                    TornarMenu();
                    break;
                case '2':
                    // Recuperar Usuari
                    RecuperarUsuari();
                    TornarMenu();
                    break;
                case '3':
                    // Modificar Usuari
                    ModificarUsuari();
                    TornarMenu();
                    break;
                case '4':
                    // Eliminar Usuari
                    EliminarUsuari();
                    TornarMenu();
                    break;
                case '5':
                    //Mostrar Agenda
                    MostrarAgenda();
                    TornarMenu();
                    break;
                case '6':
                    // Ordenar Agenda
                    OrdenarAgenda();
                    TornarMenu();
                    break;
                case 'q':
                    // Sortir
                
                    break;
            }
        }

        //Metode Crear Fitxer
        static void CrearFitxer()
        {
            StreamWriter sW;
            if (!File.Exists("agenda.txt"))
            {
                sW = new StreamWriter(@".\agenda.txt");
                sW.Close();
            }
        }

        // Mètode TornarMenu
        static void TornarMenu()
        {
            Thread.Sleep(5000);
            Console.WriteLine($"\n\n-----------------------------------------");
            Console.WriteLine($"Prem qualsevol botó per tornar a l'agenda ...");
            char tornar = Console.ReadKey().KeyChar;
        }

        // Mètode DonarAlta
        static void DonarAlta()
        {
            //Roger;Palmada;41673250B;633556238;17/04/2001;roger.palmada@gmail.com
            StreamWriter sW;
            sW = new StreamWriter(new FileStream(@".\agenda.txt", FileMode.Append));

            string nom, cognom, dni, telefon, dNaixa, correu, dades;
            Titol("DONAR D'ALTA USUARI");
            // Demana nom i cognom
            Console.Write("Introdueix el teu nom: ");
            nom = Console.ReadLine();
            nom = EscriureNom(nom);

            Console.Write("Introdueix el teu cognom: ");
            cognom = Console.ReadLine();
            cognom = cognom;

            // Demana el DNI
            dni = DemanarDni();

            // Demana el telefon
            telefon = DemanarTelefon();

            // Demana la data de naixament
            dNaixa = DemanarData();

            // Demana el correu electronic
            correu = DemanarCorreu();

            // Escriure les Dades
            dades = nom + ';' + cognom + ';' + dni + ';' + telefon + ';' + dNaixa + ';' + correu;
            sW.WriteLine(dades);
            sW.Close();
            Console.Write("\n");
            MostrarDadesUserFriendly(dades);
        }

        //Mètode Recuperar usuari
        static void RecuperarUsuari()
        {
            StreamReader sR = new StreamReader(@".\agenda.txt");
            string nomCognomDemanar, nom, cognom, nomCognom, linia, liniaAux, dades = "";
            bool trobat = false;
            Titol("RECUPERAR USUARI");
            Console.WriteLine("Introdueix el nom i cognom de la persona que vulguis mostrar");
            nomCognomDemanar = Console.ReadLine();

            while (!sR.EndOfStream && !trobat)
            {
                linia = sR.ReadLine();
                nom = linia.Substring(0, linia.IndexOf(';'));
                liniaAux = linia.Substring(linia.IndexOf(';') + 1);
                cognom = liniaAux.Substring(0, liniaAux.IndexOf(';'));
                nomCognom = nom + ' ' + cognom;

                if (nomCognom == nomCognomDemanar)
                {
                    dades = linia;
                    trobat = true;
                }
            }
            if (!trobat) Console.WriteLine($"L'usuari {nomCognomDemanar} no existeix");
            else
            {
                MostrarDadesUserFriendly(dades);
            }
        }

        // Mètode MostrarDadesUserFriendly
        static void MostrarDadesUserFriendly(string dades)
        {
            string nom, cognom, dni, telefon, dataNaix, correu;
            DateTime data;
            CultureInfo catala = new CultureInfo("ca-ES");
            // dades = Roger;Palmada;41673251B;633556238;17/04/2001;roger.palmada@gmail.com
            nom = dades.Substring(0, dades.IndexOf(';'));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            cognom = dades.Substring(0, dades.IndexOf(';'));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            dni = dades.Substring(0, dades.IndexOf(';'));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            telefon = dades.Substring(0, dades.IndexOf(";"));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            dataNaix = dades.Substring(0, dades.IndexOf(';'));
            correu = dades.Substring(dades.IndexOf(';') + 1);

            data = Convert.ToDateTime(dataNaix);

            Console.WriteLine(
                $"\tUsuari:     {nom} {cognom}\n" +
                $"\tDataNaix:   {data.ToString("dddd, dd MMMM yyyy", catala)}\n" +
                $"\tDNI:        {dni}\n" +
                $"\tTelèfon:    {telefon}\n" +
                $"\tCorreu:     {correu}\n");
        }

        static void MostrarDadesCompacte(string dades)
        {
            string nom, cognom, dni, telefon, dataNaix, correu;
            // dades = Roger;Palmada;41673251B;633556238;17/04/2001;roger.palmada@gmail.com
            nom = dades.Substring(0, dades.IndexOf(';'));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            cognom = dades.Substring(0, dades.IndexOf(';'));
            dades = dades.Substring(dades.IndexOf(';') + 1);
            dades = dades.Substring(dades.IndexOf(';') + 1);
            telefon = dades.Substring(0, dades.IndexOf(";"));
            Console.Write($"\tUsuari:   {nom} {cognom}".PadRight(40, ' '));
            Console.WriteLine($"Telèfon: {telefon}");

        }


        //Mètode Modificar Usuari
        static void ModificarUsuari()
        {
            StreamReader sR = new StreamReader(@".\agenda.txt");
            StreamWriter sW;
            string nomCognomMod, linia, nom, cognom, telefon = "", dni = "", dataNaix = "", correu = "", nomCognom = "1234", agenda = "", dades = "";
            char opcio = '0', opcio2='0';
            bool trobat = false;
            Titol("MODIFICAR USUARI");
            Console.WriteLine("Introdueix el nom i cognom de l'usuari que vulguis modificar: (ex: 'Roger palmada')");
            nomCognomMod = Console.ReadLine();
            nom = nomCognomMod.Substring(0, nomCognomMod.IndexOf(' '));
            cognom = nomCognomMod.Substring(nomCognomMod.IndexOf(' ') + 1);

            // Busca el nom i cognom al fitxer
            while (!sR.EndOfStream)
            {

                linia = sR.ReadLine();
                if (linia.Contains(nom) && linia.Contains(cognom))
                {
                    trobat = true;
                }
            }
            sR.Close();
            linia = "";
            sR = new StreamReader(@".\agenda.txt");

            if (!trobat) Console.WriteLine("No s'ha trobat el nom i cognom al fitxer");
            else
            {
                while (!SiONo(ref opcio))
                {
                    Console.Clear();
                    Titol("modificar usuari");
                    Console.WriteLine($"Segur que vols modificar a l'usuari {nomCognom}? ('S' / 'N') ");
                    opcio = Console.ReadKey().KeyChar;

                }
                Console.Clear();
                Titol("modificar usuari");
                switch (opcio)
                {
                    case 's':

                        while (nomCognomMod != nomCognom && !sR.EndOfStream)
                        {
                            linia = sR.ReadLine();
                            if (linia.Contains(nom) && linia.Contains(cognom))
                            {
                                while ((opcio2 < '1' || opcio2 > '7'))
                                {
                                    Console.WriteLine("1. Nom      2. Cognom     3. DNI      4. Telefon      5. DataNaix     6. Correu      7. Sortir");
                                    Console.WriteLine("Quina dada vols modificar?");
                                    opcio2 = Console.ReadKey().KeyChar;
                                }

                                nom = linia.Substring(0, linia.IndexOf(';'));
                                linia = linia.Substring(linia.IndexOf(';') + 1);
                                cognom = linia.Substring(0, linia.IndexOf(';'));
                                linia = linia.Substring(linia.IndexOf(';') + 1);
                                dni = linia.Substring(0, linia.IndexOf(';'));
                                linia = linia.Substring(linia.IndexOf(';') + 1);
                                telefon = linia.Substring(0, linia.IndexOf(";"));
                                linia = linia.Substring(linia.IndexOf(';') + 1);
                                dataNaix = linia.Substring(0, linia.IndexOf(';'));
                                correu = linia.Substring(linia.IndexOf(';') + 1);

                                Console.Write(". ");
                                switch (opcio2)
                                {
                                    case '1':
                                        Console.WriteLine("Introdueix el teu nom");
                                        nom = Console.ReadLine();
                                        nom = EscriureNom(nom);
                                        break;
                                    case '2':
                                        Console.WriteLine("Introdueix el teu cognom");
                                        cognom = Console.ReadLine();
                                        cognom = EscriureNom(cognom);
                                        break;
                                    case '3':
                                        dni = DemanarDni();
                                        break;
                                    case '4':
                                        telefon = DemanarTelefon();
                                        break;
                                    case '5':
                                        dataNaix = DemanarData();
                                        break;
                                    case '6':
                                        correu = DemanarCorreu();
                                        break;
                                    case '7':
                                        break;
                                }
                                dades = $"{nom};{cognom};{dni};{telefon};{dataNaix};{correu}";
                                ReescriureAgenda(ref agenda, dades);

                            }
                            else
                            {
                                ReescriureAgenda(ref agenda, dades);
                            }

                        }
                        MostrarDadesUserFriendly(dades);
                        sR.Close();
                        agenda += "\n";
                        sW = new StreamWriter(@".\agenda.txt");
                        sW.Write(agenda);
                        sW.Close();

                        break;
                    case 'n':
                        Console.WriteLine("No es modificarà l'usuari...");
                        break;
                }
            }
        }


        //Mètode Eliminar Usuari
        static void EliminarUsuari()
        {
            StreamReader sR;
            string nomCognom, linia = "", liniaAux = "", nomFitxer = "", cognomFitxer = "", nomCognomFitxer = "", agenda = "";
            char opcio = '0';
            sR = new StreamReader(@".\agenda.txt");
            Titol("ELIMINAR USUARI");
            Console.WriteLine("Escriu el nom i cognom de l'usuari que vulguis eliminar:");
            nomCognom = Console.ReadLine();

            while (!SiONo(ref opcio))
            {
                Console.Clear();
                Console.WriteLine($"Segur que vols eliminar a l'usuari {nomCognom}? ('S' / 'N') ");
                opcio = Console.ReadKey().KeyChar;

            }

            switch (opcio)
            {
                case 's':
                    while (!sR.EndOfStream)
                    {
                        // linia = Roger;Palmada;41673251B;633556238;17/04/2001;roger.palmada@gmail.com
                        // liniaAux = Palmada;41673251B;633556238;17/04/2001;roger.palmada@gmail.com
                        linia = sR.ReadLine();
                        nomFitxer = linia.Substring(0, linia.IndexOf(';'));
                        liniaAux = linia.Substring(linia.IndexOf(';') + 1);
                        cognomFitxer = liniaAux.Substring(0, liniaAux.IndexOf(';'));
                        nomCognomFitxer = nomFitxer + ' ' + cognomFitxer;

                        if (nomCognom != nomCognomFitxer)
                        {
                            ReescriureAgenda(ref agenda, linia);
                        }
                    }
                    sR.Close();
                    agenda += "\n";
                    SobreescriureFitxer(agenda);

                    break;
                case 'n':
                    Console.WriteLine($"No s'eliminarà a {nomCognom}...");
                    break;
            }

        }

        //Mètode Ordenar Agenda
        static void OrdenarAgenda()
        {
            StreamReader sR;
            int i = 0;
            string dades, dadesPetit = "ZZ", dadesAnt = "AA", agenda = "";
            sR = new StreamReader(@".\agenda.txt");
            Titol("ORDENAR AGENDAD");
            while (i < RangAgenda(sR))
            {
                sR = new StreamReader(@".\agenda.txt");
                while (!sR.EndOfStream)
                {
                    dades = sR.ReadLine();
                    if (dades.CompareTo(dadesPetit) < 0 && dades.CompareTo(dadesAnt) > 0)
                    {
                        dadesPetit = dades;
                    }
                }
                ReescriureAgenda(ref agenda, dadesPetit);
                dadesAnt = dadesPetit;
                dadesPetit = "ZZ";
                i++;
                sR.Close();
            }
            sR.Close();
            agenda += "\n";
            Console.WriteLine(agenda);
            Console.WriteLine("El fitxer 'agenda.txt' ha estat ordenat afabèticament.");
            SobreescriureFitxer(agenda);
        }

        //Mètode Mostrar Agenda
        static void MostrarAgenda()
        {
            StreamReader sR = new StreamReader(@".\agenda.txt");
            int i = 0, rang = 0;
            string dades = "", dadesPetit = "ZZ", dadesAnt = "AA";
            Titol("MOSTRAR AGENDA");

            // Llegeix el fitxer tants cops com rang hi hagi, cada cop escollint el nombre més petit (no més petit que l'anterior) i escribint-lo
            while (i < RangAgenda(sR))
            {
                sR = new StreamReader(@".\agenda.txt");
                while (!sR.EndOfStream)
                {
                    dades = sR.ReadLine();
                    if (dades.CompareTo(dadesPetit) < 0 && dades.CompareTo(dadesAnt) > 0)
                    {
                        dadesPetit = dades;
                    }
                }
                MostrarDadesCompacte(dadesPetit);
                dadesAnt = dadesPetit;
                dadesPetit = "ZZ";
                i++;
                sR.Close();
            }
        }

        // Mètode Reescriure a l'agenda buida
        static void ReescriureAgenda(ref string agenda, string linia)
        {
            if (agenda == "") agenda += linia;
            else agenda += "\n" + linia;
        }

        // Mètode Rang Agenda
        static int RangAgenda(StreamReader sR)
        {
            int rang = 0;
            sR = new StreamReader(@".\agenda.txt");
            while (!sR.EndOfStream)
            {
                sR.ReadLine();
                rang++;
            }
            sR.Close();
            return rang;
        }

        // Mètode Demanar DNI
        static string DemanarDni()
        {
            string dni;
            Console.Write("Introdueix el teu número de DNI (format: '41673251B'): ");
            dni = Console.ReadLine();
            while (!ValidarDni(dni))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu número de DNI (format: '41673251B'); ");

                dni = Console.ReadLine();
            }
            return dni;
        }


        // Mètode validar DNI
        static bool ValidarDni(string dni)
        {
            bool valid = false;
            Regex patro = new Regex("^[1-9][0-9]{7}[A-Z]$");
            //Declaracio variables
            string dniNumeros = dni.Substring(0, 8);
            string dniLletra = dni.Substring(8, 1);
            string dniLletraReal = CalcularDNILletra(Convert.ToInt32(dniNumeros));

            //Comparació de les dos lletres del DNI
            if (patro.IsMatch(dni) && dniLletraReal == dniLletra)
            {
                valid = true;
            }
            return valid;
        }

        // Mètode Demanar Telefon
        static string DemanarTelefon()
        {
            string telefon = "";

            Console.Write("Introdueix el teu número de telèfon (format: '972265402'): ");
            telefon = Console.ReadLine();

            while (!ValidarTelefon(telefon))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu número de telèfon (format: '972265402'): ");
                telefon = Console.ReadLine();
            }
            return telefon;
        }

        static string CalcularDNILletra(int dniNumeros)
        {
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            int residu = dniNumeros % 23;
            return control[residu];
        }

        // Mètode validar telèfon
        static bool ValidarTelefon(string telefon)
        {
            bool valid = false;
            Regex patro = new Regex("^[1-9][0-9]{8}$");
            if (patro.IsMatch(telefon)) valid = true;
            return valid;
        }

        // Mètode Escriure Nom
        static string EscriureNom(string nom)
        {
            string nouNom;
            nouNom = nom.Substring(0, 1).ToUpper() + nom.Substring(1).ToLower();
            return nouNom;
        }

        // Demanar Correu Electrònic
        static string DemanarCorreu()
        {
            string correu;

            Console.Write("Introdueix el teu correu electrònic: ");
            correu = Console.ReadLine();

            while (!ValidarCorreu(correu))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu correu electrònic: ");
                correu = Console.ReadLine();
            }
            return correu;

        }

        // Mètode Validar Correu 
        static bool ValidarCorreu(string correu)
        {
            bool valid = false;
            Regex patro = new Regex(@"^[a-z0-9.\-_]{3,}[@][a-z.]{3,}[/.](com||es)$");
            if (patro.IsMatch(correu)) valid = true;
            return valid;
        }

        // Demanar Data
        static string DemanarData()
        {
            string data;

            Console.Write("Introdueix la teva data de naixament (format: 'dd/mm/yyyy'): ");
            data = Console.ReadLine();

            while (!ValidarData(data))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir la teva data de naixament (format: 'dd/mm/yyyy'): ");
                data = Console.ReadLine();
            }
            return data;

        }

        // Mètode Validar Data
        static bool ValidarData(string dataString)
        {
            bool valid = false;
            DateTime dataTime;

            if (DateTime.TryParse(dataString, out dataTime))
            {
                valid = true;
            }
            return valid;
        }

        // Mètode SobreescriureFitxer
        static void SobreescriureFitxer(string text)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(@".\agenda.txt");
            info.Attributes = System.IO.FileAttributes.Normal;
            System.IO.File.Delete(@"agenda.txt");
            StreamWriter sW;
            sW = new StreamWriter(@".\agenda.txt");
            sW.WriteLine(text);
            sW.Close();
        }

        // DECORACIÓ
        // Mètode Pintar Agenda
        static void PintarAgenda(string agenda)
        {
            string linea = "", text = agenda;
            while (text.Contains("\n"))
            {
                linea = text.Substring(0, text.IndexOf("\n"));
                Centrar(linea);
                text = text.Substring(text.IndexOf("\n") + 1);
            }
            Centrar(text);
        }
        static void PintarAgenda(string agenda, char i)
        {
            string linea = "", text = agenda;
            while (text.Contains("\n"))
            {
                linea = text.Substring(0, text.IndexOf("\n"));
                Centrar(linea, i);
                text = text.Substring(text.IndexOf("\n") + 1);
            }
            Centrar(text);
        }

        // Mètode Centrar
        static void Centrar(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) - (text.Length / 2) - 1) + "}", ""));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(String.Format($"{text}"));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        static void Centrar(string text, char i)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) - (text.Length / 2) - 1) + "}", ""));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            if (text.Contains((i)))
            {
                Console.Write(text.Substring(0, text.IndexOf(i)));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(String.Format($"{text.Substring(3, text.Length - 4)}"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('║');
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(String.Format($"{text}"));
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }


        // Mètode Pintar
        static void Pintar(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(String.Format("{0," + (text.Length - 1) + "}", ""));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(String.Format($"{text}"));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        // Mètode PintarOpcio
        static void PintarOpcio(string agenda, char i)
        {
            PintarAgenda(Agenda(), i);
            Thread.Sleep(1000);
            Console.Clear();
        }

        // Mètode Títol
        static void Titol(string text)
        {
            text = text.ToUpper();
            Console.WriteLine();
            PintarAgenda($"~~~~~~~~~~~~~~~~~~{text}~~~~~~~~~~~~~~~~~~");
        }
    }
}
