// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*function ChangeColor() {
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
console.log(extractOnlyFish(animals));//fish saja*/

//asynchronous javascript
$(document).ready(function () {

    $(`#mainTable`).DataTable({

        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon/",
            dataType: "JSON",
            dataSrc: "results"

        },

        columns: [

            {
                data: 'url',
                render: function (data, type, row) {
                    let number = data.split('/')[6];
                    return number;
                }
            },

            { data: "name" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="detail('${data.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke" class="btn btn-primary">Detail</button></td>`;
                }
            }
        ]
    });
});

function detail(stringURL) {
    $.ajax({
        url: stringURL
    }).done(res => {
        $(".modal-title").html(res.name);
        $(".pokemon-img").attr("src", res.sprites.front_default);

        let types = "";
        res.types.forEach((type) => {
            let typeColor = getTypeColor(type.type.name)
            types += `<span class="badge rounded-pill" style="background-color: ${typeColor}">${type.type.name}</span>`;
        });
        $(".pokemon-type").html(types);

        let abilities = "";
        res.abilities.forEach((ability) => {
            abilities += `<li class="list-group-item">${ability.ability.name}</li>`;
        });
        $(".pokemon-abilities").html(abilities);

        $("#hp").css("width", res.stats[0].base_stat + "%").attr("aria-valuenow", res.stats[0].base_stat).html("HP: " + res.stats[0].base_stat);
        $("#attack").css("width", res.stats[1].base_stat + "%").attr("aria-valuenow", res.stats[1].base_stat).html("Attack: " + res.stats[1].base_stat);
        $("#defense").css("width", res.stats[2].base_stat + "%").attr("aria-valuenow", res.stats[2].base_stat).html("Defense: " + res.stats[2].base_stat);
        $("#sattack").css("width", res.stats[3].base_stat + "%").attr("aria-valuenow", res.stats[3].base_stat).html("Spesial Attack: " + res.stats[3].base_stat);
        $("#sdefense").css("width", res.stats[4].base_stat + "%").attr("aria-valuenow", res.stats[4].base_stat).html("Spesial Defense: " + res.stats[4].base_stat);
        $("#speed").css("width", res.stats[5].base_stat + "%").attr("aria-valuenow", res.stats[5].base_stat).html("Speed: " + res.stats[5].base_stat);
    });

    function getTypeColor(typeName) {
        let typeColor = "";
        switch (typeName) {
            case "water":
                typeColor = "#B0E0E6";
                break;
            case "fire":
                typeColor = "#FF8C00";
                break;
            case "grass":
                typeColor = "#228B22";
                break;
            case "poison":
                typeColor = "#4B0082";
                break;
            case "flying":
                typeColor = "#778899";
                break;
            case "bug":
                typeColor = "#008080";
                break;
            case "normal":
                typeColor = "#696969";
                break;
        }
        return typeColor;
    }
}

