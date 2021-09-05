using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//LeanTween
using DG.Tweening;

public class Demo : MonoBehaviour
{
    public Image loading;
    private void Start()
    {
        Admin admin = new Admin { Name = "ProPTIT" };
        Admin admin2 = new Admin { Name = "PTIT" };

        Person p1 = new Person { Name = "Tai" };
        p1.RegisterChanel(admin);

        Person p2 = new Person { Name = "Doanh" };
        p2.RegisterChanel(admin2);
        p2.RegisterChanel(admin);

        Person p3 = new Person { Name = "Nga" };
        p3.RegisterChanel(admin);

        Person p4 = new Person { Name = "Luc" };
        p4.RegisterChanel(admin);

        admin.UploadNewVideo("Lap trinh voi Unity");

        admin2.UploadNewVideo("Lap trinh voi C++");
        /*
         * GAMEMASTER. MusicOnClick (list)
         Scene game 
            Button MUSIC => Ảnh chuyển sang off
        // Script 
        // MusicOnClick.Add(MusicOnClick) Register
            private MusicOnClick(){
                chuyen ảnh => off
                tắt music ở scene đó đi
            }    



            Button MUSIC/Setting, MUSIC/Pause
            //MusicOnClick.Add(MusicOnClick)
                private MusicOnClick(){
                    chuyen ảnh => off
                    tắt music ở scene đó đi
                    Thêm mặt mếu khi mình tắt music
                }    

        //Observer - một lần chạy cho tất cả
        //Nhược điểm - Time

            100 Button music => click 1 lần => 100 btn => dạng off
        1 Button MUSIC ở MENU click => {
            GAMEMASTER.MusicOnClick.Run();
        }
         
         
         */
    }
}


public interface INotity
{
    void Notify(string valueText, Admin admin);
}

public class Person : INotity // Subcriber
{

    private List<Admin> admin = new List<Admin>();

    public string Name { get; set; }

    public void RegisterChanel(Admin admin)
    {
        if (!this.admin.Contains(admin))
        {
            this.admin.Add(admin);
        }
        Observer.listActions.Add(this);
    }

    public void CancelRegisterChanel(Admin admin)
    {
        Observer.listActions.Remove(this);
    }

    public void Notify(string valueText, Admin admin) // Nguoi ban dang ki da gui 1 video len
    {
        if (this.admin.Contains(admin))
        {
            Debug.Log(Name + "\t" + valueText);
        }
    }
}

public static class Observer // Server
{
    public static List<INotity> listActions = new List<INotity>();

    public static void SendMessage(string valueText, Admin admin)
    {
        foreach (INotity notify in listActions)
        {
            notify.Notify(valueText, admin);
        }
    }
}

public class Admin // Chu tai khoan Youtube
{
    public string Name { get; set; }
    public void UploadNewVideo(string title)
    {
        Observer.SendMessage("\t" + Name + " Da gui cho ban mot video - " + title + "\n", this);
    }
}