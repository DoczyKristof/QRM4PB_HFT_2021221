let cinemas = []
let connection
let cinemaUpdate
let cinemashavemovie = []
let roomswithmovies = []
let avgprice

getdata()
setupSignalR()

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
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

    await fetch('http://localhost:20463/stat/AverageMoviePrice')
        .then(x => x.json())
        .then(result => {
            avgprice = result
            console.log(avgprice)
            displaystat()
        })

    await fetch('http://localhost:20463/stat/CinemasThatHaveMovie')
        .then(x => x.json())
        .then(result => {
            cinemashavemovie = result
            console.log(cinemashavemovie)
            displaystat()
        })

    await fetch('http://localhost:20463/stat/RoomsThatHaveMovie')
        .then(x => x.json())
        .then(result => {
            roomswithmovies = result
            console.log(roomswithmovies)
            displaystat()
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

function displaystat() {
    document.getElementById('statarea').innerHTML = null
    document.getElementById('statarea').innerHTML +=
        "<p>Average movie price: " + avgprice + "HUF</p><br />"

    document.getElementById('statarea2').innerHTML = null
    document.getElementById('statarea2').innerHTML +=
        "<p>Number of rooms playing movies: " + roomswithmovies.length + "</p><br />"

    document.getElementById('moviearea').innerHTML = null
    cinemashavemovie.forEach(
        (cinema) => {
            console.log(cinema)
            document.getElementById('moviearea').innerHTML +=
                "<tr><td>" + cinema.name + "</td></tr>"
        })
}

function create() {
    let name = document.getElementById('cinemaname').value

    //let numofrooms = document.getElementById('numofrooms').value
    //let index = 0
    //var array = []
    //while (index < numofrooms) {
    //    array.push(Room.call(Rindex++));
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


//function createRooms(roomNumber, cinemaid) {
//    //let numofrooms = document.getElementById('numofrooms').value

//    fetch('http://localhost:20463/Room', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//        },
//        body: JSON.stringify(
//            {
//                RoomNumber = roomNumber,
//                CinemaId = cinemaid
//            }),
//    })
//        .then(response => response)
//        .then(data => {
//        })
//        .catch((error) => {
//            console.error('Error:', error);
//        })
//}

function remove(id) {
    if (confirm("Cinema number " + id + " will be deleted")) {
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
    } else {
        return
    }
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


function changePage(page) {
    window.location.href = page
}


//function Room() {
//    this.RoomNumber = ''
//}

