using System;
using System.IO;
using System.Text.Json;

namespace tpmodul8_103022300104
{
    public class BankTransferConfig
    {
        private const string ConfigPath = "bank_transfer_config.json";
        public BankTransferConfig config;

        public string lang { get; set; } = "en";

        public Transfer transfer { get; set; }

        public List<string> methods { get; set; } = ["RTO(real-time)","SKN","RTGS","BI FAST"];

        public Confirmation confirmation { get; set; }  
       

       public BankTransferConfig(string lang, Transfer transfer, List<string> method, Confirmation confirmation)
        {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = method;
            this.confirmation = confirmation;
        }

        public class Transfer {
            public int threshold { get; set; } = 25000000;
            public int low_fee { get; set; } = 6500;
            public int high_fee { get; set; } = 15000;
        }

        public class Confirmation
        {
            public string en { get; set; } = "yes";
            public string id { get; set; } = "ya";

            public Confirmation (string en, string id)
            {
                this.en = en;
                this.id = id;
            }
        }
        public BankTransferConfig Load()
        {
            String configJsonData = File.ReadAllText(ConfigPath);
            config = JsonSerializer.Deserialize<BankTransferConfig>(configJsonData); return config;

        }

        public void setDefault()
        {
           
            Confirmation confirmation = new Confirmation("yes", "ya");
            List<string> methods = new List<string>(["RTO(real-time)", "SKN", "RTGS", "BI FAST"]);  


            config.lang = "en";
            config.transfer.threshold = 25000000;
            config.transfer.low_fee = 6500;
            config.transfer.high_fee = 15000;
            config.methods = ["RTO(real-time)", "SKN", "RTGS", "BI FAST"]; ;
            config.confirmation.en = "yes";
            config.confirmation.id = "id"; 
        }

        public void WriteNewConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(ConfigPath, jsonString);
        }

      
    }
}