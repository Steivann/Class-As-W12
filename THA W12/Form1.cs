using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Sqlconnection = "server=localhost;uid=root;pwd=Ayamgoreng1025;database=premier_league";
        public MySqlConnection sqlconnect = new MySqlConnection(Sqlconnection);
        public MySqlCommand sqlcommand;
        public MySqlDataAdapter mydataadapter;
        public MySqlDataReader sqlDataReader;
        public string sqlquery;
        DataTable dn = new DataTable();
        DataTable dt = new DataTable();
        DataTable dmt = new DataTable();
        DataTable dp = new DataTable();
        DataTable dk = new DataTable();
        DataTable dd = new DataTable();
        DataTable dz = new DataTable();
        DataTable da = new DataTable();
        DataTable dpl = new DataTable();
        DataTable ds = new DataTable();
        DataTable ds1 = new DataTable();
        DataTable dm = new DataTable();
        DataTable dm1 = new DataTable();

        private void DGVADD()
        {
            da.Clear();
            try
            {
                string command = "select * From Player;";
                sqlcommand = new MySqlCommand(command, sqlconnect);
                mydataadapter = new MySqlDataAdapter(sqlcommand);
                mydataadapter.Fill(da);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updatedgvmanager()
        {
            dm = new DataTable();
            sqlquery = $"select m.manager_name, t.team_name, m.birthdate, n.nation from manager m inner join team t on m.manager_id = t.manager_id inner join nationality n on m.nationality_id = n.nationality_id where team_id =  '{comboBoxE_teamname.SelectedValue.ToString()}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dm);
            dataGridView1.DataSource = dm;

            dm1 = new DataTable();
            sqlquery = $"select m.manager_name, m.birthdate, n.nation from manager m inner join nationality n on m.nationality_id = n.nationality_id where working = '0'";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dm1);
            dataGridView2.DataSource = dm1;

        }
        private void updateDGVdel()
        {
            dd.Clear();
            try
            {
                sqlquery = $"select player_name, n.nation, p.team_number, p.height, p.weight, p.birthdate\r\nfrom player p, nationality n\r\nwhere team_id = '{comboBoxD_teamname.SelectedValue.ToString()}' and status = '1';";
                sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
                mydataadapter = new MySqlDataAdapter(sqlcommand);
                mydataadapter.Fill(dd);
                dataGridView3.DataSource = dd;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlquery = "select nation, nationality_id from premier_league.nationality;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dn);
            comboBox_nation.DataSource = dn;
            comboBox_nation.ValueMember = "nationality_id";
            comboBox_nation.DisplayMember = "nation";

            sqlquery = "select team_name, team_id from premier_league.team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dpl);
            comboBoxP_tname.DataSource = dpl;
            comboBoxP_tname.ValueMember = "team_id";
            comboBoxP_tname.DisplayMember = "team_name";

            sqlquery = "select team_name, team_id from premier_league.team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dmt);
            comboBoxE_teamname.DataSource = dmt;
            comboBoxE_teamname.ValueMember = "team_id";
            comboBoxE_teamname.DisplayMember = "team_name";

            sqlquery = "select team_name, team_id from premier_league.team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dz);
            comboBoxD_teamname.DataSource = dz;
            comboBoxD_teamname.ValueMember = "team_id";
            comboBoxD_teamname.DisplayMember = "team_name";

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxD_teamname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dd.Clear();
            sqlquery = $"select p.player_name, n.nation, p.playing_pos, p.team_Number, p.height, p.weight, p.birthdate from player p inner join nationality n on p.nationality_id = n.nationality_id where status = '1'  and p.team_id = '{comboBoxD_teamname.SelectedValue.ToString()}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dd);
            dataGridView3.DataSource = dd;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView3.CurrentCell.RowIndex;
            DataGridViewRow row = dataGridView3.Rows[rowindex];
            string pnct = row.Cells[0].Value.ToString();
            sqlquery = $"update player set status = 0 where player_name = '{pnct}'";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dd);
            dataGridView3.DataSource = dd;
            updateDGVdel();
            dd.Clear();
            sqlquery = $"select p.player_name, n.nation, p.playing_pos, p.team_Number, p.height, p.weight, p.birthdate from player p inner join nationality n on p.nationality_id = n.nationality_id where status = '1'  and p.team_id = '{comboBoxE_teamname.SelectedValue.ToString()}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dd);
            dataGridView3.DataSource = dd;
        }

        private void comboBoxE_teamname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dk.Clear();
            dp.Clear();

            sqlquery = $"select m.manager_name, t.team_name, m.birthdate, n.nation from manager m, team t, nationality n where m.working = '1' and m.manager_id = t.manager_id and n.nationality_id = m.nationality_id and team_id = '{comboBoxE_teamname.SelectedValue}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dk);
            dataGridView1.DataSource = dk;


            sqlquery = $"select m.manager_name, m.birthdate, n.nation, m.manager_id \r\nfrom manager m, nationality n \r\nwhere m.working = '0'  and n.nationality_id = m.nationality_id ;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            mydataadapter = new MySqlDataAdapter(sqlcommand);
            mydataadapter.Fill(dp);
            dataGridView2.DataSource = dp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            string value = row.Cells[0].Value.ToString();
            string valuess = row.Cells[3].Value.ToString();
            string command = $"update manager\r\nset working = '1' \r\nwhere manager_name = '{value}';";
            string coba = $"update team\r\nset manager_id = '{valuess}' \r\nwhere team_id = '{comboBoxE_teamname.SelectedValue}';";
            
            try
            {
                sqlconnect.Open();
                sqlcommand = new MySqlCommand(command, sqlconnect);
                sqlDataReader = sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlconnect.Close();
            }
            try
            {
                sqlconnect.Open();
                sqlcommand = new MySqlCommand(coba, sqlconnect);
                sqlDataReader = sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlconnect.Close();
            }
            dp.Clear();
            try
            {
                sqlquery = $"select m.manager_name, t.team_name, m.birthdate, n.nation, m.manager_id \r\nfrom manager m, team t, nationality n \r\nwhere m.working = '0'  and n.nationality_id = m.nationality_id ;";
                sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
                mydataadapter = new MySqlDataAdapter(sqlcommand);
                mydataadapter.Fill(dp);
                dataGridView2.DataSource = dp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int rowIndex1 = dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow row1 = dataGridView1.Rows[rowIndex1];
            string value1 = row1.Cells[0].Value.ToString();
            // MessageBox.Show(value.ToString());
            string commands = $"update manager\r\nset working = '0' \r\nwhere manager_name = '{value1}';";
            try
            {
                sqlconnect.Open();
                sqlcommand = new MySqlCommand(commands, sqlconnect);
                sqlDataReader = sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlconnect.Close();
            }
            dk.Clear();
            try
            {
                sqlquery = $"select m.manager_name, t.team_name, m.birthdate, n.nation from manager m, team t, nationality n where m.working = '1' and m.manager_id = t.manager_id and n.nationality_id = m.nationality_id and team_id = '{comboBoxE_teamname.SelectedValue}';";
                sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
                mydataadapter = new MySqlDataAdapter(sqlcommand);
                mydataadapter.Fill(dk);
                dataGridView1.DataSource = dk;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = $"insert into player values('{textBox_pid.Text}', '{textBox_timnum.Text}', '{textBox_name.Text}', '{comboBox_nation.SelectedValue.ToString()}', '{textBox_pos.Text}', '{textBox_height.Text}', '{textBox_weight.Text}', '{dateTimePicker1.Value.ToString("yyyy--MM--dd")}', '{comboBoxP_tname.SelectedValue.ToString()}', '1', '0');";
            try
            {
                sqlconnect.Open();
                sqlcommand = new MySqlCommand(command, sqlconnect);
                sqlDataReader = sqlcommand.ExecuteReader();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }

            finally

            {
                sqlconnect.Close();
                DGVADD();
            }

        }
    }
}
