// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ChangeColor() {
    let tes1 = document.getElementById("tes1")
    let randomColor = Math.floor(Math.random() * 16777215).toString(16)
    tes1.style.backgroundColor = "#" + randomColor;

}


function ChangeTextAndFont() {
    let fonts = ['Arial', 'Verdana', 'Helvetica', 'Times New Roman', 'Courier New'];
    let tes2 = document.getElementById('tes2');
    let randomIndex = Math.floor(Math.random() * fonts.length);
    tes2.style.fontFamily = fonts[randomIndex];
    tes2.textContent = "TES TES TES TES TES TES TES TES";
    tes2.style.fontSize = "30px";
}

function ChangeBackgroundColorAndText() {
    let fonts = ['Arial', 'Verdana', 'Helvetica', 'Times New Roman', 'Courier New'];
    let tes3 = document.getElementById('tes3');
    tes3.style.backgroundColor = "aqua";
    let randomColor = Math.floor(Math.random() * 16777215).toString(16);
    let randomIndex = Math.floor(Math.random() * fonts.length);
    tes3.style.fontFamily = fonts[randomIndex];
    tes3.style.backgroundColor = "#" + randomColor;
    tes3.textContent = "TES TES TES TES TES TES TES TES";
    tes3.style.fontSize = "30px";

}

//array of object
const animals = [
    { name: "dory", species: "fish", class: { name: "vertebrata" } },
    { name: "tom", species: "cat", class: { name: "mamalia" } },
    { name: "nemo", species: "fish", class: { name: "vertebrata" } },
    { name: "umar", species: "cat", class: { name: "mamalia" } },
    { name: "gary", species: "fish", class: { name: "human" } },
];

//fungsi 1: jika species nya 'cat' maka ambil lalu pindahkan ke variabel OnlyCat
function extractOnlyCat(animalsList) {
    const onlyCat = [];
    for (let i = 0; i < animalsList.length; i++) {
        const animal = animalsList[i];
        if (animal.species === 'cat') {
            onlyCat.push(animal);
        }
    }
    return onlyCat;
}

//fungsi 2: jika species nya 'fish' maka ganti class -> menjadi 'non-mamalia'
function extractOnlyFish(animalsList) {
    const onlyFish = [];
    for (let i = 0; i < animalsList.length; i++) {
        const animal = animalsList[i];
        if (animal.species === 'fish') {
            animal.class.name = 'non-mamalia';
            onlyFish.push(animal);
        }
    }
    return onlyFish;
}

//output
console.log(animals);//memanggil semua animal
console.log(extractOnlyCat(animals));//cat saja
console.log(extractOnlyFish(animals));//fish saja

//asynchronous javascript
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += "<li>" + val.name + "</li>";
    })
    $("#listSW").html(temp);
});

//TABEL
$.ajax({
    url: "https://swapi.dev/api/people/",
    method: "GET",
    success: function (data) {
        var tbody = document.getElementById("tbodySW");
        var temp = "";

        var count = 0;

        for (var i = 0; i < data.results.length; i++) {
            var character = data.results[i];
            if (character.gender !== "n/a") {
                count++;
                if (count > 5) {
                    break;
                }

                var row = document.createElement("tr");

                var noCell = document.createElement("td");
                noCell.textContent = count;
                row.appendChild(noCell);

                var nameCell = document.createElement("td");
                nameCell.textContent = character.name;
                row.appendChild(nameCell);

                var heightCell = document.createElement("td");
                heightCell.textContent = character.height + " cm";
                row.appendChild(heightCell);

                var genderCell = document.createElement("td");
                genderCell.textContent = character.gender;
                row.appendChild(genderCell);

                tbody.appendChild(row);
            }
        }
    },
    error: function () {
        alert("Failed to fetch data from the Star Wars API.");
    }
});


