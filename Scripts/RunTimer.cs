using Godot;
using System;

public partial class RunTimer : RichTextLabel
{
    private float elapsedTime = 0f;
    private bool isRunnning = false;

    public override void _Ready()
    {
        Text = "00:00";
    }
    public override void _Process(double delta)
    {
        if(isRunnning){
            elapsedTime += (float)delta;
            Text = GetFormattedTime();
        }
    }

    public void StartRun(){
        elapsedTime = 0f;
        isRunnning = true;
    }

    public void StopRun(){
        isRunnning = false;
    }

    public float getTime(){
        return elapsedTime;
    }

    public string GetFormattedTime(){
        int minutes = (int)elapsedTime/60;
        int seconds = (int)elapsedTime%60;
        //int milliseconds = (int)(elapsedTime * 100) % 100;

        return $"{minutes:D2}:{seconds:D2}";//.{milliseconds:D2}";
    }
}
