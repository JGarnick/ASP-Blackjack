using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blackjack : System.Web.UI.Page
{
    Deck deck;
    BJHand pHand;
    BJHand dHand;  

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (IsPostBack)
        {
            deck = (Deck)Session["deck"];
            pHand = (BJHand)Session["pHand"];
            dHand = (BJHand)Session["dHand"];                      
        }

        else
        {
            deck = new Deck();
            pHand = new BJHand();
            dHand = new BJHand();

            deck.Shuffle();

            pHand.AddCard(deck.Deal());
            pHand.AddCard(deck.Deal());

            dHand.AddCard(deck.Deal());
            dHand.AddCard(deck.Deal());

            dScore.Text = dHand.Score.ToString();
            pScore.Text = pHand.Score.ToString();

            messageTB.Text = "Would you like to Hit or Stay?";

            dCard1.ImageUrl = "Images/Cards/black_back.jpg";
            dCard2.ImageUrl = "Images/Cards/" + dHand.GetCard(1).FileName;
            

            pCard1.ImageUrl = "Images/Cards/" + pHand.GetCard(0).FileName;
            pCard2.ImageUrl = "Images/Cards/" + pHand.GetCard(1).FileName;
            

            Session["deck"] = deck;
            Session["pHand"] = pHand;
            Session["dHand"] = dHand;
        }
       

        
    }
    protected void hitBtn_Click(object sender, EventArgs e)
    {
        pHand.AddCard(deck.Deal());
        pScore.Text = pHand.Score.ToString();
        if (pHand.NumCards >= 3)
        {
            pCard3.Visible = true;
            pCard3.ImageUrl = "Images/Cards/" + pHand.GetCard(2).FileName;
        }
        if (pHand.NumCards == 4)
        {
            pCard4.Visible = true;
            pCard4.ImageUrl = "Images/Cards/" + pHand.GetCard(3).FileName;
        }
        if (pHand.IsBusted)
        {
            hitBtn.Enabled = false;
            stayBtn.Enabled = false;
            messageTB.Text = "Dealer Wins!";
        }
    }
    protected void stayBtn_Click(object sender, EventArgs e)
    {
        stayBtn.Enabled = false;
        hitBtn.Enabled = false;
        dCard1.ImageUrl = "Images/Cards/" + dHand.GetCard(0).FileName;
        dCard2.ImageUrl = "Images/Cards/" + dHand.GetCard(1).FileName;
        while (dHand.Score <= 18)
        {
            dHand.AddCard(deck.Deal());
            dScore.Text = dHand.Score.ToString();
            if (dHand.NumCards == 3)
            {
                dCard3.Visible = true;
                dCard3.ImageUrl = "Images/Cards/" + dHand.GetCard(2).FileName;
            }

            if (dHand.NumCards == 4)
            {
                dCard4.Visible = true;
                dCard4.ImageUrl = "Images/Cards/" + dHand.GetCard(3).FileName;
            }
            if (dHand.IsBusted)
            {
                messageTB.Text = "Player Wins!";
            }
            else if (dHand.Score > pHand.Score)
                messageTB.Text = "Dealer Wins!";
        }           
    }
    protected void newGameBtn_Click(object sender, EventArgs e)
    {
        deck = new Deck();
        pHand = new BJHand();
        dHand = new BJHand();

        deck.Shuffle();

        pHand.AddCard(deck.Deal());
        pHand.AddCard(deck.Deal());

        dHand.AddCard(deck.Deal());
        dHand.AddCard(deck.Deal());

        dScore.Text = dHand.Score.ToString();
        pScore.Text = pHand.Score.ToString();

        stayBtn.Enabled = true;
        hitBtn.Enabled = true;

        dCard3.Visible = false;
        dCard4.Visible = false;
        pCard3.Visible = false;
        pCard4.Visible = false;

        messageTB.Text = "Would you like to Hit or Stay?";

        dCard1.ImageUrl = "Images/Cards/black_back.jpg";
        dCard2.ImageUrl = "Images/Cards/" + dHand.GetCard(1).FileName;

        pCard1.ImageUrl = "Images/Cards/" + pHand.GetCard(0).FileName;
        pCard2.ImageUrl = "Images/Cards/" + pHand.GetCard(1).FileName;


        Session["deck"] = deck;
        Session["pHand"] = pHand;
        Session["dHand"] = dHand;

    }
}