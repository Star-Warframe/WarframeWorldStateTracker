using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarframeWorldStateTest
{
    public partial class Form1 : Form
    {
        WorldStateData wsdata;

        public Form1(WorldStateData ws)
        {
            InitializeComponent();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            wsdata = ws;
            comboBox1.DataSource = new BindingSource(wsdata.varList, null);

            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            comboBox2.DataSource = new BindingSource(wsdata.platformList, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsdata.refreshWorldState();
            comboBox1_SelectionChangeCommitted(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Count() >= 1)
            {
                Application.OpenForms.OfType<Form2>().ElementAt(0).Close();
            }
            Form2 form2 = new Form2(wsdata);
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox1_SelectionChangeCommitted(null, null);
            comboBox2.SelectedIndex = 0;
            label1.Text = "Current time: " + DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            textBox1.Text = "";

            switch (comboBox1.SelectedValue as string)
            {
                case "events":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (Event ev in wsdata.events)
                        {
                            str.AppendLine(ev.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "alerts":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (Alert al in wsdata.alerts)
                        {
                            str.AppendLine(al.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "sorties":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (Sorties so in wsdata.sorties)
                        {
                            str.AppendLine(so.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "syndicateMissions":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (SyndicateMission sm in wsdata.syndicateMissions)
                        {
                            str.AppendLine(sm.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "fissureMissions":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (FissureMission fm in wsdata.fissureMissions)
                        {
                            str.AppendLine(fm.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "flashSales":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (FlashSale fs in wsdata.flashSales)
                        {
                            str.AppendLine(fs.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "invasions":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (Invasion iv in wsdata.invasions)
                        {
                            str.AppendLine(iv.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "nodeOverrides":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (NodeOverride no in wsdata.nodeOverrides)
                        {
                            str.AppendLine(no.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "badlandNodes":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (BadlandNode bn in wsdata.badlandNodes)
                        {
                            str.AppendLine(bn.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "voidTraders":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (VoidTrader vt in wsdata.voidTraders)
                        {
                            str.AppendLine(vt.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "primeVaultAvailabilities":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (string pv in wsdata.primeVaultAvailabilities)
                        {
                            str.AppendLine(pv.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "dailyDeals":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (DailyDeal dd in wsdata.dailyDeals)
                        {
                            str.AppendLine(dd.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "pvpChallengeInstances":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (PVPChallengeInstance pc in wsdata.pvpChallengeInstances)
                        {
                            str.AppendLine(pc.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "pvpAlternativeModes":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (PVPAlternativeMode pa in wsdata.pvpAlternativeModes)
                        {
                            str.AppendLine(pa.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "persistentEnemies":
                    {
                        StringBuilder str = new StringBuilder();
                        foreach (PersistentEnemy pe in wsdata.persistentEnemies)
                        {
                            str.AppendLine(pe.ToString());
                        }
                        textBox1.Text = str.ToString();
                        break;
                    }
                case "projectPct":
                    {
                        StringBuilder str = new StringBuilder();
                        // projectPct[0] is Fomorian progress
                        // projectPct[1] is Razorback Armada progress
                        // projectPct[2] is ???

                        if (wsdata.projectPct != null)
                        {
                            str.AppendLine("Fomorian: " + wsdata.projectPct[0].ToString("N1") + "%");
                            str.AppendLine("Razorback Armada: " + wsdata.projectPct[1].ToString("N1") + "%");
                        }

                        textBox1.Text = str.ToString();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Current time: " + DateTime.Now.ToString("hh:mm:ss tt");

            if ((DateTime.Now.Second % 60) == 0)    // refresh every 60 seconds with timer interval of 1000
            {
                wsdata.refreshWorldState();
                comboBox1_SelectionChangeCommitted(null, null);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox2.SelectedValue);
            wsdata.changePlatform(comboBox2.SelectedValue.ToString());
            wsdata.refreshWorldState();
            comboBox1_SelectionChangeCommitted(null, null);
        }
    }
}
