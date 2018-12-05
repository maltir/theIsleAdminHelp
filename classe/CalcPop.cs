using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TheIsleAdminHelp.enumObj;
using WinSCP;

namespace TheIsleAdminHelp.classe
{
    public class CalcPop
    {
        private const string DIR_PLAYERS = "TheIsle/Saved/Databases/Survival/Players";
        private List<Dino> playerDinoList;

        public Stat Calcul(ProgressBar progress)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = Properties.Settings.Default.host,
                UserName = Properties.Settings.Default.user,
                Password = Properties.Settings.Default.password,
                PortNumber = Properties.Settings.Default.port,
            };

            using (Session session = new Session())
            {
                playerDinoList = new List<Dino>();
                // Connect
                session.Open(sessionOptions);

                // Get list of files in the directory
                RemoteDirectoryInfo directoryInfo = session.ListDirectory(DIR_PLAYERS);
                progress.Minimum = 0;
                progress.Maximum = directoryInfo.Files.Count;
                progress.Value = 0;

                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                transferOptions.OverwriteMode = OverwriteMode.Overwrite;

                foreach (RemoteFileInfo fileInfo in directoryInfo.Files)
                {
                    if (fileInfo.LastWriteTime >= new DateTime(2018, 12, 01))
                    {
                        TransferOperationResult transferResult = session.GetFiles(fileInfo.FullName + "*", "temp.json", false, transferOptions);
                        // Throw on any error
                        transferResult.Check();
                        if (File.Exists(@"temp.json"))
                        {
                            using (StreamReader r = new StreamReader(@"temp.json"))
                            {
                                string json = r.ReadToEnd();
                                try
                                {
                                    string idTypeDino = "";
                                    bool genre = false;
                                    Race espece = Race.Autre;
                                    dynamic array = JsonConvert.DeserializeObject(json);
                                    foreach (var item in array)
                                    {
                                        if (item.Name == "CharacterClass")
                                        {
                                            idTypeDino = item.Value.Value;
                                        }
                                        if (item.Name == "bGender")
                                        {
                                            genre = item.Value.Value;
                                        }
                                    }
                                    foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
                                    {
                                        if (idTypeDino.Contains(Enum.GetName(typeof(Race), race)))
                                        {
                                            espece = race;
                                        }
                                    }
                                    playerDinoList.Add(new Dino(espece, genre, idTypeDino));
                                }
                                catch (JsonReaderException)
                                {
                                    //do nothing c'est les "fichiers" dossier
                                }
                            }
                            File.Delete(@"temp.json");
                        }
                    }
                    progress.Value++;
                }
            }
            return CalculStat();
        }

        private Stat CalculStat()
        {
            Stat stat = new Stat();
            foreach(Dino d in playerDinoList)
            {
                if (d.Carni)
                {
                    stat.Carni++;
                }
                else
                {
                    stat.Herbi++;
                }

                switch (d.Race)
                {
                    case Race.Rex:
                        stat.Rex++;
                        if (d.Genre) stat.RexF++; else stat.RexM++;
                        if (d.Bebe)
                        {
                            stat.RexBB++;
                            if (d.Genre) stat.RexBBF++; else stat.RexBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.RexJuv++;
                            if (d.Genre) stat.RexJuvF++; else stat.RexJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.RexSub++;
                            if (d.Genre) stat.RexSubF++; else stat.RexSubM++;
                        }
                        else
                        {
                            stat.RexAdult++;
                            if (d.Genre) stat.RexAdultF++; else stat.RexAdultM++;
                        }
                        break;
                    case Race.Allo:
                        stat.Allo++;
                        if (d.Genre) stat.AlloF++; else stat.AlloM++;
                        if (d.Bebe)
                        {
                            stat.AlloBB++;
                            if (d.Genre) stat.AlloBBF++; else stat.AlloBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AlloJuv++;
                            if (d.Genre) stat.AlloJuvF++; else stat.AlloJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AlloSub++;
                            if (d.Genre) stat.AlloSubF++; else stat.AlloSubM++;
                        }
                        else
                        {
                            stat.AlloAdult++;
                            if (d.Genre) stat.AlloAdultF++; else stat.AlloAdultM++;
                        }
                        break;
                    case Race.Carno:
                        stat.Carno++;
                        if (d.Genre) stat.CarnoF++; else stat.CarnoM++;
                        if (d.Bebe)
                        {
                            stat.CarnoBB++;
                            if (d.Genre) stat.CarnoBBF++; else stat.CarnoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.CarnoJuv++;
                            if (d.Genre) stat.CarnoJuvF++; else stat.CarnoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.CarnoSub++;
                            if (d.Genre) stat.CarnoSubF++; else stat.CarnoSubM++;
                        }
                        else
                        {
                            stat.CarnoAdult++;
                            if (d.Genre) stat.CarnoAdultF++; else stat.CarnoAdultM++;
                        }
                        break;
                    case Race.Cerato:
                        stat.Cerato++;
                        if (d.Genre) stat.CeratoF++; else stat.CeratoM++;
                        if (d.Bebe)
                        {
                            stat.CeratoBB++;
                            if (d.Genre) stat.CeratoBBF++; else stat.CeratoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.CeratoJuv++;
                            if (d.Genre) stat.CeratoJuvF++; else stat.CeratoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.CeratoSub++;
                            if (d.Genre) stat.CeratoSubF++; else stat.CeratoSubM++;
                        }
                        else
                        {
                            stat.CeratoAdult++;
                            if (d.Genre) stat.CeratoAdultF++; else stat.CeratoAdultM++;
                        }
                        break;
                    case Race.Dilo:
                        stat.Dilo++;
                        if (d.Genre) stat.DiloF++; else stat.DiloM++;
                        if (d.Bebe)
                        {
                            stat.DiloBB++;
                            if (d.Genre) stat.DiloBBF++; else stat.DiloBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.DiloJuv++;
                            if (d.Genre) stat.DiloJuvF++; else stat.DiloJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.DiloSub++;
                            if (d.Genre) stat.DiloSubF++; else stat.DiloSubM++;
                        }
                        else
                        {
                            stat.DiloAdult++;
                            if (d.Genre) stat.DiloAdultF++; else stat.DiloAdultM++;
                        }
                        break;
                    case Race.Giga:
                        stat.Giga++;
                        if (d.Genre) stat.GigaF++; else stat.GigaM++;
                        if (d.Bebe)
                        {
                            stat.GigaBB++;
                            if (d.Genre) stat.GigaBBF++; else stat.GigaBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.GigaJuv++;
                            if (d.Genre) stat.GigaJuvF++; else stat.GigaJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.GigaSub++;
                            if (d.Genre) stat.GigaSubF++; else stat.GigaSubM++;
                        }
                        else
                        {
                            stat.GigaAdult++;
                            if (d.Genre) stat.GigaAdultF++; else stat.GigaAdultM++;
                        }
                        break;
                    case Race.Utah:
                        stat.Utah++;
                        if (d.Genre) stat.UtahF++; else stat.UtahM++;
                        if (d.Bebe)
                        {
                            stat.UtahBB++;
                            if (d.Genre) stat.UtahBBF++; else stat.UtahBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.UtahJuv++;
                            if (d.Genre) stat.UtahJuvF++; else stat.UtahJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.UtahSub++;
                            if (d.Genre) stat.UtahSubF++; else stat.UtahSubM++;
                        }
                        else
                        {
                            stat.UtahAdult++;
                            if (d.Genre) stat.UtahAdultF++; else stat.UtahAdultM++;
                        }
                        break;
                    case Race.Acro:
                        stat.Acro++;
                        if (d.Genre) stat.AcroF++; else stat.AcroM++;
                        if (d.Bebe)
                        {
                            stat.AcroBB++;
                            if (d.Genre) stat.AcroBBF++; else stat.AcroBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AcroJuv++;
                            if (d.Genre) stat.AcroJuvF++; else stat.AcroJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AcroSub++;
                            if (d.Genre) stat.AcroSubF++; else stat.AcroSubM++;
                        }
                        else
                        {
                            stat.AcroAdult++;
                            if (d.Genre) stat.AcroAdultF++; else stat.AcroAdultM++;
                        }
                        break;
                    case Race.Spino:
                        stat.Spino++;
                        if (d.Genre) stat.SpinoF++; else stat.SpinoM++;
                        if (d.Bebe)
                        {
                            stat.SpinoBB++;
                            if (d.Genre) stat.SpinoBBF++; else stat.SpinoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.SpinoJuv++;
                            if (d.Genre) stat.SpinoJuvF++; else stat.SpinoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.SpinoSub++;
                            if (d.Genre) stat.SpinoSubF++; else stat.SpinoSubM++;
                        }
                        else
                        {
                            stat.SpinoAdult++;
                            if (d.Genre) stat.SpinoAdultF++; else stat.SpinoAdultM++;
                        }
                        break;
                    case Race.Velociraptor:
                        stat.Velociraptor++;
                        if (d.Genre) stat.VelociraptorF++; else stat.VelociraptorM++;
                        if (d.Bebe)
                        {
                            stat.VelociraptorBB++;
                            if (d.Genre) stat.VelociraptorBBF++; else stat.VelociraptorBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.VelociraptorJuv++;
                            if (d.Genre) stat.VelociraptorJuvF++; else stat.VelociraptorJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.VelociraptorSub++;
                            if (d.Genre) stat.VelociraptorSubF++; else stat.VelociraptorSubM++;
                        }
                        else
                        {
                            stat.VelociraptorAdult++;
                            if (d.Genre) stat.VelociraptorAdultF++; else stat.VelociraptorAdultM++;
                        }
                        break;
                    case Race.Sucho:
                        stat.Sucho++;
                        if (d.Genre) stat.SuchoF++; else stat.SuchoM++;
                        if (d.Bebe)
                        {
                            stat.SuchoBB++;
                            if (d.Genre) stat.SuchoBBF++; else stat.SuchoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.SuchoJuv++;
                            if (d.Genre) stat.SuchoJuvF++; else stat.SuchoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.SuchoSub++;
                            if (d.Genre) stat.SuchoSubF++; else stat.SuchoSubM++;
                        }
                        else
                        {
                            stat.SuchoAdult++;
                            if (d.Genre) stat.SuchoAdultF++; else stat.SuchoAdultM++;
                        }
                        break;
                    case Race.Herrera:
                        stat.Herrera++;
                        if (d.Genre) stat.HerreraF++; else stat.HerreraM++;
                        if (d.Bebe)
                        {
                            stat.HerreraBB++;
                            if (d.Genre) stat.HerreraBBF++; else stat.HerreraBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.HerreraJuv++;
                            if (d.Genre) stat.HerreraJuvF++; else stat.HerreraJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.HerreraSub++;
                            if (d.Genre) stat.HerreraSubF++; else stat.HerreraSubM++;
                        }
                        else
                        {
                            stat.HerreraAdult++;
                            if (d.Genre) stat.HerreraAdultF++; else stat.HerreraAdultM++;
                        }
                        break;
                    case Race.Bary:
                        stat.Bary++;
                        if (d.Genre) stat.BaryF++; else stat.BaryM++;
                        if (d.Bebe)
                        {
                            stat.BaryBB++;
                            if (d.Genre) stat.BaryBBF++; else stat.BaryBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.BaryJuv++;
                            if (d.Genre) stat.BaryJuvF++; else stat.BaryJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.BarySub++;
                            if (d.Genre) stat.BarySubF++; else stat.BarySubM++;
                        }
                        else
                        {
                            stat.BaryAdult++;
                            if (d.Genre) stat.BaryAdultF++; else stat.BaryAdultM++;
                        }
                        break;
                    case Race.Austro:
                        stat.Austro++;
                        if (d.Genre) stat.AustroF++; else stat.AustroM++;
                        if (d.Bebe)
                        {
                            stat.AustroBB++;
                            if (d.Genre) stat.AustroBBF++; else stat.AustroBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AustroJuv++;
                            if (d.Genre) stat.AustroJuvF++; else stat.AustroJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AustroSub++;
                            if (d.Genre) stat.AustroSubF++; else stat.AustroSubM++;
                        }
                        else
                        {
                            stat.AustroAdult++;
                            if (d.Genre) stat.AustroAdultF++; else stat.AustroAdultM++;
                        }
                        break;
                    case Race.Albert:
                        stat.Albert++;
                        if (d.Genre) stat.AlbertF++; else stat.AlbertM++;
                        if (d.Bebe)
                        {
                            stat.AlbertBB++;
                            if (d.Genre) stat.AlbertBBF++; else stat.AlbertBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AlbertJuv++;
                            if (d.Genre) stat.AlbertJuvF++; else stat.AlbertJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AlbertSub++;
                            if (d.Genre) stat.AlbertSubF++; else stat.AlbertSubM++;
                        }
                        else
                        {
                            stat.AlbertAdult++;
                            if (d.Genre) stat.AlbertAdultF++; else stat.AlbertAdultM++;
                        }
                        break;
                    case Race.Diablo:
                        stat.Diablo++;
                        if (d.Genre) stat.DiabloF++; else stat.DiabloM++;
                        if (d.Bebe)
                        {
                            stat.DiabloBB++;
                            if (d.Genre) stat.DiabloBBF++; else stat.DiabloBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.DiabloJuv++;
                            if (d.Genre) stat.DiabloJuvF++; else stat.DiabloJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.DiabloSub++;
                            if (d.Genre) stat.DiabloSubF++; else stat.DiabloSubM++;
                        }
                        else
                        {
                            stat.DiabloAdult++;
                            if (d.Genre) stat.DiabloAdultF++; else stat.DiabloAdultM++;
                        }
                        break;
                    case Race.Dryo:
                        stat.Dryo++;
                        if (d.Genre) stat.DryoF++; else stat.DryoM++;
                        if (d.Bebe)
                        {
                            stat.DryoBB++;
                            if (d.Genre) stat.DryoBBF++; else stat.DryoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.DryoJuv++;
                            if (d.Genre) stat.DryoJuvF++; else stat.DryoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.DryoSub++;
                            if (d.Genre) stat.DryoSubF++; else stat.DryoSubM++;
                        }
                        else
                        {
                            stat.DryoAdult++;
                            if (d.Genre) stat.DryoAdultF++; else stat.DryoAdultM++;
                        }
                        break;
                    case Race.Galli:
                        stat.Galli++;
                        if (d.Genre) stat.GalliF++; else stat.GalliM++;
                        if (d.Bebe)
                        {
                            stat.GalliBB++;
                            if (d.Genre) stat.GalliBBF++; else stat.GalliBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.GalliJuv++;
                            if (d.Genre) stat.GalliJuvF++; else stat.GalliJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.GalliSub++;
                            if (d.Genre) stat.GalliSubF++; else stat.GalliSubM++;
                        }
                        else
                        {
                            stat.GalliAdult++;
                            if (d.Genre) stat.GalliAdultF++; else stat.GalliAdultM++;
                        }
                        break;
                    case Race.Maia:
                        stat.Maia++;
                        if (d.Genre) stat.MaiaF++; else stat.MaiaM++;
                        if (d.Bebe)
                        {
                            stat.MaiaBB++;
                            if (d.Genre) stat.MaiaBBF++; else stat.MaiaBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.MaiaJuv++;
                            if (d.Genre) stat.MaiaJuvF++; else stat.MaiaJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.MaiaSub++;
                            if (d.Genre) stat.MaiaSubF++; else stat.MaiaSubM++;
                        }
                        else
                        {
                            stat.MaiaAdult++;
                            if (d.Genre) stat.MaiaAdultF++; else stat.MaiaAdultM++;
                        }
                        break;
                    case Race.Para:
                        stat.Para++;
                        if (d.Genre) stat.ParaF++; else stat.ParaM++;
                        if (d.Bebe)
                        {
                            stat.ParaBB++;
                            if (d.Genre) stat.ParaBBF++; else stat.ParaBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.ParaJuv++;
                            if (d.Genre) stat.ParaJuvF++; else stat.ParaJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.ParaSub++;
                            if (d.Genre) stat.ParaSubF++; else stat.ParaSubM++;
                        }
                        else
                        {
                            stat.ParaAdult++;
                            if (d.Genre) stat.ParaAdultF++; else stat.ParaAdultM++;
                        }
                        break;
                    case Race.Trike:
                        stat.Trike++;
                        if (d.Genre) stat.TrikeF++; else stat.TrikeM++;
                        if (d.Bebe)
                        {
                            stat.TrikeBB++;
                            if (d.Genre) stat.TrikeBBF++; else stat.TrikeBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.TrikeJuv++;
                            if (d.Genre) stat.TrikeJuvF++; else stat.TrikeJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.TrikeSub++;
                            if (d.Genre) stat.TrikeSubF++; else stat.TrikeSubM++;
                        }
                        else
                        {
                            stat.TrikeAdult++;
                            if (d.Genre) stat.TrikeAdultF++; else stat.TrikeAdultM++;
                        }
                        break;
                    case Race.Pachy:
                        stat.Pachy++;
                        if (d.Genre) stat.PachyF++; else stat.PachyM++;
                        if (d.Bebe)
                        {
                            stat.PachyBB++;
                            if (d.Genre) stat.PachyBBF++; else stat.PachyBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.PachyJuv++;
                            if (d.Genre) stat.PachyJuvF++; else stat.PachyJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.PachySub++;
                            if (d.Genre) stat.PachySubF++; else stat.PachySubM++;
                        }
                        else
                        {
                            stat.PachyAdult++;
                            if (d.Genre) stat.PachyAdultF++; else stat.PachyAdultM++;
                        }
                        break;
                    case Race.Shan:
                        stat.Shan++;
                        if (d.Genre) stat.ShanF++; else stat.ShanM++;
                        if (d.Bebe)
                        {
                            stat.ShanBB++;
                            if (d.Genre) stat.ShanBBF++; else stat.ShanBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.ShanJuv++;
                            if (d.Genre) stat.ShanJuvF++; else stat.ShanJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.ShanSub++;
                            if (d.Genre) stat.ShanSubF++; else stat.ShanSubM++;
                        }
                        else
                        {
                            stat.ShanAdult++;
                            if (d.Genre) stat.ShanAdultF++; else stat.ShanAdultM++;
                        }
                        break;
                    case Race.Camara:
                        stat.Camara++;
                        if (d.Genre) stat.CamaraF++; else stat.CamaraM++;
                        if (d.Bebe)
                        {
                            stat.CamaraBB++;
                            if (d.Genre) stat.CamaraBBF++; else stat.CamaraBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.CamaraJuv++;
                            if (d.Genre) stat.CamaraJuvF++; else stat.CamaraJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.CamaraSub++;
                            if (d.Genre) stat.CamaraSubF++; else stat.CamaraSubM++;
                        }
                        else
                        {
                            stat.CamaraAdult++;
                            if (d.Genre) stat.CamaraAdultF++; else stat.CamaraAdultM++;
                        }
                        break;
                    case Race.Ava:
                        stat.Ava++;
                        if (d.Genre) stat.AvaF++; else stat.AvaM++;
                        if (d.Bebe)
                        {
                            stat.AvaBB++;
                            if (d.Genre) stat.AvaBBF++; else stat.AvaBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AvaJuv++;
                            if (d.Genre) stat.AvaJuvF++; else stat.AvaJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AvaSub++;
                            if (d.Genre) stat.AvaSubF++; else stat.AvaSubM++;
                        }
                        else
                        {
                            stat.AvaAdult++;
                            if (d.Genre) stat.AvaAdultF++; else stat.AvaAdultM++;
                        }
                        break;
                    case Race.Anky:
                        stat.Anky++;
                        if (d.Genre) stat.AnkyF++; else stat.AnkyM++;
                        if (d.Bebe)
                        {
                            stat.AnkyBB++;
                            if (d.Genre) stat.AnkyBBF++; else stat.AnkyBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.AnkyJuv++;
                            if (d.Genre) stat.AnkyJuvF++; else stat.AnkyJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.AnkySub++;
                            if (d.Genre) stat.AnkySubF++; else stat.AnkySubM++;
                        }
                        else
                        {
                            stat.AnkyAdult++;
                            if (d.Genre) stat.AnkyAdultF++; else stat.AnkyAdultM++;
                        }
                        break;
                    case Race.Oro:
                        stat.Oro++;
                        if (d.Genre) stat.OroF++; else stat.OroM++;
                        if (d.Bebe)
                        {
                            stat.OroBB++;
                            if (d.Genre) stat.OroBBF++; else stat.OroBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.OroJuv++;
                            if (d.Genre) stat.OroJuvF++; else stat.OroJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.OroSub++;
                            if (d.Genre) stat.OroSubF++; else stat.OroSubM++;
                        }
                        else
                        {
                            stat.OroAdult++;
                            if (d.Genre) stat.OroAdultF++; else stat.OroAdultM++;
                        }
                        break;
                    case Race.Psittaco:
                        stat.Psittaco++;
                        if (d.Genre) stat.PsittacoF++; else stat.PsittacoM++;
                        if (d.Bebe)
                        {
                            stat.PsittacoBB++;
                            if (d.Genre) stat.PsittacoBBF++; else stat.PsittacoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.PsittacoJuv++;
                            if (d.Genre) stat.PsittacoJuvF++; else stat.PsittacoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.PsittacoSub++;
                            if (d.Genre) stat.PsittacoSubF++; else stat.PsittacoSubM++;
                        }
                        else
                        {
                            stat.PsittacoAdult++;
                            if (d.Genre) stat.PsittacoAdultF++; else stat.PsittacoAdultM++;
                        }
                        break;
                    case Race.Puerta:
                        stat.Puerta++;
                        if (d.Genre) stat.PuertaF++; else stat.PuertaM++;
                        if (d.Bebe)
                        {
                            stat.PuertaBB++;
                            if (d.Genre) stat.PuertaBBF++; else stat.PuertaBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.PuertaJuv++;
                            if (d.Genre) stat.PuertaJuvF++; else stat.PuertaJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.PuertaSub++;
                            if (d.Genre) stat.PuertaSubF++; else stat.PuertaSubM++;
                        }
                        else
                        {
                            stat.PuertaAdult++;
                            if (d.Genre) stat.PuertaAdultF++; else stat.PuertaAdultM++;
                        }
                        break;
                    case Race.Stego:
                        stat.Stego++;
                        if (d.Genre) stat.StegoF++; else stat.StegoM++;
                        if (d.Bebe)
                        {
                            stat.StegoBB++;
                            if (d.Genre) stat.StegoBBF++; else stat.StegoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.StegoJuv++;
                            if (d.Genre) stat.StegoJuvF++; else stat.StegoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.StegoSub++;
                            if (d.Genre) stat.StegoSubF++; else stat.StegoSubM++;
                        }
                        else
                        {
                            stat.StegoAdult++;
                            if (d.Genre) stat.StegoAdultF++; else stat.StegoAdultM++;
                        }
                        break;
                    case Race.Therizino:
                        stat.Therizino++;
                        if (d.Genre) stat.TherizinoF++; else stat.TherizinoM++;
                        if (d.Bebe)
                        {
                            stat.TherizinoBB++;
                            if (d.Genre) stat.TherizinoBBF++; else stat.TherizinoBBM++;
                        }
                        else if (d.Juvie)
                        {
                            stat.TherizinoJuv++;
                            if (d.Genre) stat.TherizinoJuvF++; else stat.TherizinoJuvM++;
                        }
                        else if (d.Subs)
                        {
                            stat.TherizinoSub++;
                            if (d.Genre) stat.TherizinoSubF++; else stat.TherizinoSubM++;
                        }
                        else
                        {
                            stat.TherizinoAdult++;
                            if (d.Genre) stat.TherizinoAdultF++; else stat.TherizinoAdultM++;
                        }
                        break;
                    default:
                        stat.Autre++;
                        break;
                }
            }
            return stat;
        }
    }
}
