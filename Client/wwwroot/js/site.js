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
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += "<li>" + val.name + "</li>";
    })
    $("#listSW").html(temp);
});

$(document).ready(function () {
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon/"
    }).done((result) => {
        let temp = "";
        $.each(result.results, (key, val) => {
            temp += `
                <tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>${val.url}</td>
                    <td>
                        <button onclick="detailPokemon('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPokemon" class="btn btn-primary">Detail</button>
                    </td>
                </tr>
            `;
        })
        $("#tbodyPokemon").html(temp);
    });
});

function detailPokemon(stringURL) {
    $.ajax({
        url: stringURL,
        success: (result) => {
            $('.modal-title').html(result.name);
            $('#pokemonImage').attr('src', result.sprites.front_default);
            $('#pokemonImage').css('max-width', '100%');
            $('.details').html(`
                <p><strong>Height:</strong> ${result.height}</p>
                <p><strong>Weight:</strong> ${result.weight}</p>
                <p><strong>Base Experience:</strong> ${result.base_experience}</p>
                <!-- Add more details as needed -->
            `);
        }
    });
}
