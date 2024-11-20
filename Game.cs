using Godot;
using System;


public partial class Game : Node2D
{
	private ConfigFile CF = new ConfigFile();
	[Export]
	public String CFName = "Config.ini";
	
	int upgradedpoints = 1;
	int points = 0;
	int pointspersecond = 0;
	int upgradecost = 10;
	int ppccost = 20;

	Label pointcountlabel;
	Label pointpersecondlabel;
	Button upgradecostbutton;
	Button ppcbutton;

	

	public override void _Ready()
	{
		if (CF.Load("user://"+CFName) != Error.Ok)
			{
				SaveGame();
			}
		else
			{
				Load();
			}
		
		pointcountlabel = (Label)GetNode("Node/CanvasLayer/Control/Pointsbox/points");
		pointpersecondlabel = (Label)GetNode("Node/CanvasLayer/Control/Pointsbox/pointspersecond");
		upgradecostbutton = (Button)GetNode("Node/CanvasLayer/VBoxContainer/upgradebutton");
		ppcbutton = (Button)GetNode("Node/CanvasLayer/VBoxContainer/PPCbutton");
		
		
		pointcountlabel.Text = points.ToString();
		pointpersecondlabel.Text = pointspersecond + "/s";
		upgradecostbutton.Text = "Upgrade cost: " + upgradecost;
		ppcbutton.Text = "Per-Click cost: " + ppccost;

	}
	
	public void Load()
		{
			GD.Print("Loading data from: " + OS.GetUserDataDir()+"/"+CFName);
			points = (int)CF.GetValue("Amount", "points", points);
			upgradedpoints = (int)CF.GetValue("Amount", "upgradedpoints", upgradedpoints);
			pointspersecond = (int)CF.GetValue("Amount", "pointspersecond", pointspersecond);
			upgradecost = (int)CF.GetValue("Amount", "upgradecost", upgradecost);
			ppccost = (int)CF.GetValue("Amount", "ppccost", ppccost);
			
		}
	private void _on_reset_button_down()
		{
			CF.SetValue("Amount", "points", 0);
			CF.SetValue("Amount", "upgradedpoints", 1);
			CF.SetValue("Amount", "pointspersecond", 0);
			CF.SetValue("Amount", "upgradecost", 10);
			CF.SetValue("Amount", "ppccost", 20);
			
			CF.Save("user://"+CFName);
			GD.Print("Saving data to: " + OS.GetUserDataDir()+"/"+CFName);
			
			Load();
			pointcountlabel.Text = points.ToString();
			pointpersecondlabel.Text = pointspersecond + "/s";
			upgradecostbutton.Text = "Upgrade cost: " + upgradecost;
			ppcbutton.Text = "Per-Click cost: " + ppccost;
		}
	// points per second upgrade button
	private void _on_upgradebutton_button_down()
		{
			
			if(points >= upgradecost)
				{
					
					points -= upgradecost;
					pointspersecond += 1;
					pointcountlabel.Text = points.ToString();
					pointpersecondlabel.Text =  pointspersecond.ToString() + "/s";
					upgradecost += 10;
					upgradecostbutton.Text = "Upgrade cost: " + upgradecost.ToString();
				}
		}
		// points per click upgrade button
	private void _on_pp_cbutton_button_down()
		{
			if(points >= ppccost)
				{
					points -= ppccost;
					upgradedpoints += 1;
					ppccost += 20;
					ppcbutton.Text = "Per-Click Cost: " + ppccost.ToString();
				}
		}
		// collect points image
	private void _on_clickbutton_button_down()
		{
			if(upgradedpoints == 1)
				{
					points += 1;
					pointcountlabel.Text = points.ToString();
				}
			if(upgradedpoints > 1)
				{
					points += upgradedpoints;
					pointcountlabel.Text = points.ToString();
				}
					
		}
	private void _on_save_button_down()
		{
			SaveGame();
		}
	
		// timer per second
	private void _on_timer_timeout()
		{
			points += pointspersecond;
			pointcountlabel.Text = points.ToString();
		}
		
	public void SaveGame()
		{
			CF.SetValue("Amount", "points", points);
			CF.SetValue("Amount", "upgradedpoints", upgradedpoints);
			CF.SetValue("Amount", "pointspersecond", pointspersecond);
			CF.SetValue("Amount", "upgradecost", upgradecost);
			CF.SetValue("Amount", "ppccost", ppccost);
			
			CF.Save("user://"+CFName);
			GD.Print("Saving data to: " + OS.GetUserDataDir()+"/"+CFName);
		}
		
	
	
}
