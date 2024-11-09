using Godot;
using System;

public partial class Game : Node2D
{
	int points = 0;
	int pointspersecond = 0;
	Label pointcountlabel;
	Label pointpersecondlabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pointcountlabel = (Label)GetNode("Node/CanvasLayer/points");
		pointpersecondlabel = (Label)GetNode("Node/CanvasLayer/pointspersecond");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
	
	private void _on_clickbutton_button_down()
		{
			points += 1;
			pointcountlabel.Text = points.ToString();
			
		}
	private void _on_upgradebutton_button_down()
		{
			if(points >= 10)
				{
					points -= 10;
					pointspersecond += 1;
					pointcountlabel.Text = points.ToString();
					pointpersecondlabel.Text =  pointspersecond.ToString() + "/s";
				}
		}
			
	private void _on_timer_timeout()
		{
			points += pointspersecond;
			pointcountlabel.Text = points.ToString();
		}
}
