using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SoccerBase
{
    /// <summary>
    /// Dictionary static properties
    /// {Country|Ivnet, SportRadar}
    /// </summary>
    class Synonyms
    {
        public static Dictionary<object, string> Months = new Dictionary<object, string>()
        {
            {"Jan","1"},
            {"Feb","2"},
            {"Mar","3"},
            {"Apr","4"},
            {"May","5"},
            {"Jun","6"},
            {"Jul","7"},
            {"Aug","8"},
            {"Sep","9"},
            {"Oct","10"},
            {"Nov","11"},
            {"Dec","12"}
        };

        public static Dictionary<object, string> Countries = new Dictionary<object, string>()
        {
            {"Holland","Netherlands"},
            {"Portuguese","Portugal"},
            {"Spanish","Spain"}
        };

        public static Dictionary<object, string> Leagues = new Dictionary<object, string>()
        {
            {"Belgium|Jupiler League","Pro League"},
            {"Denmark|SAS Ligaen","Superligaen"},
            {"Germany|Bundesliga 1","Bundesliga"},
            {"Greece|1st Katigoria","Super League"},
            {"Italy|Seria A","Serie A"},
            {"Portuguese|Liga Sagres","Liga Zon Sagres"},
            {"Romania|Divizia A","First Division"},
            {"Russia|Premier Liga","Premier League"}
        };

        public static Dictionary<object, string> Teams = new Dictionary<object, string>()
        {
            {"Belgium|Cercle_B","Cercle Brugge"},
            {"Belgium|Club_Brugge","Club Brugge"},
            {"Belgium|Gent","KAA Gent"},
            {"Belgium|Germinal","G. Beerschot"},
            {"Belgium|Mechelen","KV Mechelen"},
            {"Belgium|Mons_Bergen","Mons"},
            {"Belgium|Oud_Heverlee","OH Leuven"},
            {"Belgium|S_Truiden","St. Truiden"},
            {"Belgium|Zulte_W","Zulte"},

            {"Denmark|Kobenhavn","Copenhagen"},
            {"Denmark|Nordsjelland","Nordsjalland"},
            {"Denmark|Sonderjyske","SonderjyskE"},

            {"England|Aston_Villa","Aston Villa"},
            {"England|Manchester_C","Man City"},
            {"England|Manchester_U","Man Utd"},
            {"England|Norwich_C","Norwich"},
            {"England|Queens_Park","QPR"},
            {"England|Swansea_C","Swansea"},
            {"England|West_Bromwich","W.B.A."},
            {"England|Wolverhampton","Wolves"},

            {"France|Evian_T","Evian"},
            {"France|Lyon","Ol. Lyon"},
            {"France|Marseille","Ol. Marseille"},
            {"France|Paris SG","Paris St.-Germain"},
            {"France|Snt_Etienne","St. Etienne"},
            
            {"Germany|B_Dortmund","Dortmund"},
            {"Germany|B_Leverkusen","Leverkusen"},
            {"Germany|Bayern_M","Bayern Munich"},
            {"Germany|Koln","Cologne"},
            {"Germany|M_Gladbach","M'gladbach"},
            {"Germany|Mainz","Mainz 05"},
            {"Germany|Nurnberg","Nuremberg"},
            {"Germany|Schalke","Schalke 04"},
            {"Germany|Werder_B","Bremen"},
            
            {"Greece|AEK","AEK Athens"},
            {"Greece|Aris","Aris Thessaloniki"},
            {"Greece|Asteras","Asteras Tripolis"},
            {"Greece|Atromitos","Atromitos Athens"},
            {"Greece|Iraklis","PAS Giannina"},
            {"Greece|Pas Giannina","PAS Giannina"},
            {"Greece|Pas_Giannina","PAS Giannina"},
            {"Greece|Kerkyra","Kerkyra Corfu"},
            {"Greece|OFI","OFI Crete"},
            {"Greece|Olimpiakos_P","Olympiacos"},
            {"Greece|Panaitolikos","Panetolikos"},
            {"Greece|Panathinaikos","Panathinaikos"},
            {"Greece|Xanthi","Xanthi Skoda"},

            {"Holland|AZ_Alkmaar","Alkmaar"},
            {"Holland|De_Graafschap","De Graafschap"},
            {"Holland|Den Haag","Den Haag"},
            {"Holland|Excelsior","Excelsior R."},
            {"Holland|Heracles_A","Heracles"},
            {"Holland|NAC_Breda","Breda"},
            {"Holland|NEC_Nijmegen","Nijmegen"},
            {"Holland|PSV_Eindhoven","PSV Eindhoven"},
            {"Holland|RKC_W","Waalwijk"},

            {"Portuguese|Beira_Mar","Beira Mar"},
            {"Portuguese|Braga","Sporting Braga"},
            {"Portuguese|Gil_Vicente","Gil Vicente"},
            {"Portuguese|Guimaraes","Vitoria Guimaraes"},
            {"Portuguese|Leiria","Uniao Leiria"},
            {"Portuguese|Maritimo","Maritimo Funchal"},
            {"Portuguese|P_Ferreira","Pacos Ferreira"},
            {"Portuguese|Rio_Ave","Rio Ave"},
            {"Portuguese|Setubal","Vitoria Setubal"},

            {"Romania|Astra_P","Ploiesti"},
            {"Romania|Concordia","Concordia C"},
            {"Romania|Dinamo_B","Bucuresti"},
            {"Romania|Gaz_Metan","Gaz Metan"},
            {"Romania|Otelul","Otelul Galati"},
            {"Romania|Pandurii","Pandurii Targu"},
            {"Romania|Petrolul","P. Ploiesti"},
            {"Romania|Rapid_B","Rapid Bucuresti"},
            {"Romania|Sportul_S","Studentesc"},
            {"Romania|Steaua_B","Steaua"},
            {"Romania|Targu_M","Targu Mures"},
            {"Romania|Universitat_Cl","Universitatea C"},
            {"Romania|Vointa","Vointa Sibiu"},
            
            {"Russia|Amkar","Amkar Perm"},
            {"Russia|Anji","Anzhi Makhachkala"},
            {"Russia|CSKA","CSKA Moscow"},
            {"Russia|Dynamo","Dinamo Moscow"},
            {"Russia|Kr.Sovetov","Kryliya Sovetov"},
            {"Russia|Kuban","Kuban Krasnodar"},
            {"Russia|Lokomotiv","Lokomotiv Moscow"},
            {"Russia|Rubin","Rubin Kazan"},
            {"Russia|Spartak_M","Spartak Moscow"},
            {"Russia|Spartak_N","Spartak Nalchik"},
            {"Russia|Terek","Terek Grozny"},
            {"Russia|Tom","Tom Tomsk"},
            {"Russia|Zenit","FC Zenit"},

            {"Scotland|Dundee_U","Dundee United"},
            {"Scotland|Inverness","Inverness CT"},
            {"Scotland|St_Johnstone","Johnstone"},
            {"Scotland|St_Mirren","St Mirren"},
            
            {"Spanish|Athletic_Bil","At. Bilbao"},
            {"Spanish|Atletico_M","At. Madrid"},
            {"Spanish|Racing_S","Racing"},
            {"Spanish|Rayo_Vall","Vallecano"},
            {"Spanish|Real_Betis","Betis"},
            {"Spanish|Real_Madrid","Real Madrid"},
            {"Spanish|Real_Sociedad","Real Sociedad"},
            {"Spanish|Sevilla","FC Sevilla"},
            {"Spanish|Sporting_G","Gijon"},

            {"Turkey|Antalya","Antalyaspor"},
            {"Turkey|Bursa","Bursaspor"},
            {"Turkey|Eskisehir","Eskisehirspor"},
            {"Turkey|Istanbul","Buyuksehyr"},
            {"Turkey|Karabuk","K. Karabukspor"},
            {"Turkey|Manisa","Manisaspor"},
            {"Turkey|Samsun","Samsunspor"},
            {"Turkey|Mersin","Mersin Idman Yurdu"},
            {"Turkey|G_Antep","Gaziantepspor"},
            {"Turkey|Kayseri","Kayserispor"},
            {"Turkey|Sivas","Sivasspor"},
            {"Turkey|Trabzon","Trabzonspor"},
            {"Turkey|G_Birligi","Genclerbirligi"},
            
            {"Ukraine|Arsenal","Arsenal Kiev"},
            {"Ukraine|Chernomorets","Chernomorets Odessa"},
            {"Ukraine|Dnipro","Dnipropetrovsk"},
            {"Ukraine|Dynamo_K","Dynamo Kiev"},
            {"Ukraine|Illychivets","Il. Mariupol"},
            {"Ukraine|Karpaty","Karpaty Lviv"},
            {"Ukraine|Kryvbas","Kryvyi Rih"},
            {"Ukraine|Metalist","Metalist Kharkiv"},
            {"Ukraine|Metalurh_D","Met.  Donetsk"},
            {"Ukraine|Obolon","Obolon Kiev"},
            {"Ukraine|Olexandria","Oleksandria"},
            {"Ukraine|Shakhtar_D","Shakhtar Donetsk"},
            {"Ukraine|Tavriya","Simferopol"},
            {"Ukraine|Vorskla","Poltava"},
            {"Ukraine|Zorya","Zorya Lugansk"}
        };
    }
}
