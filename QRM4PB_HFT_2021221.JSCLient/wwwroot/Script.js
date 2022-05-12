let cinemas = []
let connection
let cinemaUpdate

getdata()
setupSignalR()

function setupSignalR() {
    connection = new signalr.HubConnectionBuilder()
        .withUrl("http://localhost:5726/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CinemaCreated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("CinemaDeleted", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("CinemaUpdated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });


    connection.onclose
        (async () => {
            await start();
        });
    start();
}



async function getdata() {
    await fetch('http://localhost:20463/cinema')
        .then(x => x.json())
        .then(list => {
            cinemas = list
            console.log(list)
            display()
        })
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function display() {
    document.getElementById('resultarea').innerHTML = null
    cinemas.forEach(
        (cinema) => {
            document.getElementById('resultarea').innerHTML +=
                "<tr><td>" + cinema.id + "</td><td>" +
            cinema.name + "</td ><td>" + cinema.rooms.length + "</td><td>" +
            `<button type="button" class="deletebtn" onclick="remove(${cinema.id})">X</button>` +
            `<button type="button" class="editbtn" onclick="edit(${cinema.id})">M</button>`
            + "</td ></tr >"
            console.log(cinema.name)
        }
    )
}

function create() {
    let name = document.getElementById('cinemaname').value

    //let numofrooms = document.getElementById('numofrooms').value
    //let index = 0
    //var array = []
    //while (index < numofrooms + 1) {
    //    array.push(Room.call(RoomNumber = index++));
    //}


    fetch('http://localhost:20463/cinema', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name,
                Rooms: null
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata()
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}

function remove(id) {
    alert("Cinema number " + id + " will be deleted")

    fetch('http://localhost:20463/cinema/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata()
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}

function edit(id) {
    document.getElementById('cinemanametoupdate').value = cinemas.find(cinema => cinema['id'] == id)['name']
    document.getElementById('updatecinemaformdiv').style.visibility = "visible"
    cinemaUpdate = id
}

function update() {
    document.getElementById('updatecinemaformdiv').style.visibility = "collapse"
    let name = document.getElementById('cinemanametoupdate').value

    fetch('http://localhost:20463/cinema', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Id: cinemaUpdate,
                Name: name,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata()
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}


//function Room() {
//    this.RoomNumber = ''
//}