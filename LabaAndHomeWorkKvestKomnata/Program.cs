using LabaAndHomeWorkKvestKomnata.Class;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

List<RoomQuest> room = new List<RoomQuest>() { new RoomQuest() { Id = Guid.NewGuid().ToString(), NameRoom ="red",PassImg="Rav-4", KolUchas="Toyota",  InfoRoom="2015"} ,
  new RoomQuest() { Id = Guid.NewGuid().ToString(),  NameRoom = "green", PassImg = "X5",  KolUchas = "BMW",   InfoRoom = "2017"} };

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var pass = request.Path;
    var respons = context.Response;
    if (pass == "/api/room" && request.Method == "GET")
    {
        //перегляд
        await getAllRoom(respons);


    }
    else if (pass == "/api/room" && request.Method == "POST")
    {
    //стоврення обєкту
      await createRoom(respons, request);
    }
    else
    {
        respons.ContentType = "text/html; charset=utf-8";
        await respons.SendFileAsync("html/QuestRoom.html");

    }
});

app.Run();

async Task getAllRoom(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(room);
}

async Task createRoom(HttpResponse httpResponse, HttpRequest httpRequest)
{
    RoomQuest roomQ = await httpRequest.ReadFromJsonAsync<RoomQuest>();
    if (roomQ != null)
    {
        roomQ.Id = Guid.NewGuid().ToString();
        room.Add(roomQ);
        await httpResponse.WriteAsJsonAsync(roomQ);

    }
    else
    {
        httpResponse.StatusCode = 400;
        await httpResponse.WriteAsJsonAsync(new { massge = "Erroe!400" });


    }
}