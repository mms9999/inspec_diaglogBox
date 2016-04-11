using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.ViewModel
{
    public class UIOutput
    {
        public string Close { get; set; }
        public string Save_Diagnostics { get; set; }
        public string Report_Diagnostics { get; set; }
        public string Intro { get; set; }
        public string MachineTextBlock { get; set; }
        public string PartNumberTextBlock { get; set; }
        public string SerialTextBlock { get; set; }
        public string FullNameTextBlock { get; set; }
        public string EMailTextBlock { get; set; }
        public string CompanyTextBlock { get; set; }
        public string DealerTextBlock { get; set; }
        public string DescriptionIssueTextBlock { get; set; }
        public string CollectionOutputTextBlock { get; set; }
        public string Information { get; set; }

        public static UIOutput GetUIDisplay()
        {
            var displayUI = new UIOutput()
            {
                Close = "Close",
                Save_Diagnostics = "Save Disagnostic",
                Report_Diagnostics = "Report Diagnostic",
                Intro = "VOILA This program will collect data for diagnostic and technical purposes. "
                + "Please enter requested information and a description of any issue in the form inputs below, "
                + "then select to save or report diagnostics to Micro-Vu Corporation.",
                MachineTextBlock = "Machine (ex: Vertex 420):",
                PartNumberTextBlock = "Part Number (ex: C2410124)",
                SerialTextBlock = "Serial (ex: vx42000123)",
                FullNameTextBlock = "Full Namem: ",
                EMailTextBlock = "Email Address: ",
                CompanyTextBlock = "Company: ",
                DealerTextBlock = "Micro-Vu Dealer: ",
                DescriptionIssueTextBlock = "Description of Issues",
                CollectionOutputTextBlock = "Collection Output",
                Information = "Information",
            };
            return displayUI;
        }
    }
}
