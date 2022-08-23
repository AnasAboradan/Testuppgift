using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learingwell
{
    public partial class Chart : Form
    {
        Chart_Data data;
        String URL= "https://www.forsakringskassan.se/fk_apps/MEKAREST/public/v1/iv-planerad/IVplaneradvardland.json";
        Dictionary<string, Countries> temporary_countries;
        public Chart()
        {
            InitializeComponent();
            data = new Chart_Data(URL);
            Initialize_Asid_panel();
            Paint += draw;
            temporary_countries = new Dictionary<string, Countries>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Initialize_Asid_panel()
        {
            int i = 1;
            // for each year create one lable and add it to asid 
            foreach (var item in data.get_years())
            {

                Button b = new Button();
                b.Location = new Point(0, 70 * i);
                b.BackColor = Color.RosyBrown;
                b.Width = 200;
                b.Height = 50;
                b.Text = item;
                asid_panel.Controls.Add(b);
                i++;
                b.Click += Btn_Year_Click;
            }
        }


        // Get necessary data based on the year from when user click on any year Button
        private void Btn_Year_Click(object sender, EventArgs e)
        {
           
            Refresh_Y_Axel();
           
            
            Button btn = (Button)sender; 

            // get the number of male and female for each country code base on the number of year
            temporary_countries = data.GetCountries_code_Amount_MaleAnadFamale(btn.Text);

            // draw the country code on the y axel
            draw_y_axel(btn.Text ); //  btn.txt contains the year

            // refresh the screen 
            this.Refresh();
            
        }

        // draw event // * trigger whenever we invoke screnn.refresh * //
        private void draw(object sender, PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.Red);
            e.Graphics.DrawLine(Pens.White, 75, 10, 75, 700); // draw x line
            draw_x_axel(); //draw x axel numbers
            e.Graphics.DrawLine(Pens.White, 75, 700, 1275, 700); // draw y line
            dorw_chart_lines(sender, e.Graphics); //draw chart lines 
        }

        private Label create_chart_lable(Color bg_c, Point p, int align, int height, int width, string name, string text)
        {
            Label l = new Label();
            l.Location = new Point(p.X + align, p.Y);
            l.Width = width;
            l.Height = height;
            l.BackColor = bg_c;
            l.Name = name;
            l.ForeColor = Color.White;
            l.Font = new Font("Arial", 8);
            l.Text = text;
            return l;
        }
        private void draw_x_axel()
        {
            for (int i = 0; i <= 340; i += 10)
            {
                Label l = create_chart_lable(Color.Black, new Point(45, 690 - ((i * 2))),0,12,25,"X",i.ToString());
                l.Name = "X";
                this.Controls.Add(l);
                
            }
        }
        
        private void draw_y_axel(string year)
        {   
            // for each country code create lable and add it to the y axel 
            int align = 10;
            foreach (var item in temporary_countries)
            {

                Label l = create_chart_lable(Color.Black, new Point(75, 710), align, 20, 32,"Y", item.Key);
                this.Controls.Add(l);
                align += 38;   
            }
        }

        private void draw_rectangle(object sender, Graphics e, Color c, int x, int y, int width, int height)
        {
            SolidBrush blueBrush = new SolidBrush(c);
            Rectangle rect = new Rectangle(x, y, width, height);
            e.FillRectangle(blueBrush, rect);
        }
        private void dorw_chart_lines(object sender, Graphics e)
        {
            //each chart line consists of a set of rectangle, each rectangle represent 10 person

            int x = 82; int amount_male; int amount_female; double remainder; int y;

            // for each country draw one read line for male and blue line for female
            foreach (var item in temporary_countries) // 
            {
               
                if (item.Value.Amount_male != "")
                {
                    y = 680; // y axel alignment for each rectangle
                    
                    //divide the number of male by 10 to get the number of rectangle.
                    amount_male = Convert.ToInt32(Convert.ToDouble(item.Value.Amount_male) / 10);

                    //draw the rectangles
                    for (int i = 0; i < amount_male; i++, y -= 20)
                    {
                        draw_rectangle(sender, e, Color.Red, x, y, 13, 18);
                    }

                    // check if there is any remaining and draw it 
                    remainder = Convert.ToDouble(item.Value.Amount_male) % 10;
                    if (remainder != 0)
                    {
                        draw_rectangle(sender, e, Color.Red, x, y + 15, 13, Convert.ToInt32(remainder));
                    }

                }

                 // do the same for female

                if (item.Value.Amount_female != "")
                {

                    y = 680;
                    amount_female = Convert.ToInt32(Convert.ToDouble(item.Value.Amount_female) / 10);
                    for (int i = 0; i < amount_female; i++, y -= 20)
                    {
                        draw_rectangle(sender, e, Color.Blue, x + 17, y, 13, 18);
                    }
                    remainder = Convert.ToDouble(item.Value.Amount_female) % 10;
                    if (remainder != 0)
                    {
                        draw_rectangle(sender, e, Color.Blue, x + 17, y + 15, 13, Convert.ToInt32(remainder));
                    }
                }

               x += 38;    
            }
        }

        //remove the y axel lable
        private void Refresh_Y_Axel()
        {
         
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Label && this.Controls[i].Name != "X")
                {
                    
                        this.Controls.Remove(this.Controls[i]);
                        i--;
                }
            }
        }
    }
}
