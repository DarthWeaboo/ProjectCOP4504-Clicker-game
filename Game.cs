using Godot;
using System;

public partial class Game : Node2D
{
	int upgradedpoints = 1;
	int points = 0;
	int pointspersecond = 0;
	int upgradecost = 10;
	int ppccost = 20;
	Label pointcountlabel;
	Label pointpersecondlabel;
	Button upgradecostbutton;
	Button ppcbutton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pointcountlabel = (Label)GetNode("Node/CanvasLayer/Control/Pointsbox/points");
		pointpersecondlabel = (Label)GetNode("Node/CanvasLayer/Control/Pointsbox/pointspersecond");
		upgradecostbutton = (Button)GetNode("Node/CanvasLayer/VBoxContainer/upgradebutton");
		ppcbutton = (Button)GetNode("Node/CanvasLayer/VBoxContainer/PPCbutton");
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
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
	private void _on_timer_timeout()
		{
			points += pointspersecond;
			pointcountlabel.Text = points.ToString();
		}
}
