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
const pokedex = document.getElementById('pokedex');
const modal = document.getElementById('modal');
const modalName = document.getElementById('modal-name');
const modalImage = document.getElementById('modal-image');
const modalNumber = document.getElementById('modal-number');
const modalTypes = document.getElementById('modal-types');
const modalAbilities = document.getElementById('modal-abilities');
const modalHP = document.getElementById('modal-hp');
const modalSpeed = document.getElementById('modal-speed');
const closeBtn = document.getElementById('close');

const fetchPokemons = async () => {
      try {
        const response = await fetch('https://pokeapi.co/api/v2/pokemon?limit=12');
        const data = await response.json();

        const pokemons = data.results;
        pokemons.forEach(pokemon => {
          fetchPokemonData(pokemon);
        });
      } catch (error) {
        console.error('Error fetching Pokemon data:', error);
      }
    };

    const fetchPokemonData = async (pokemon) => {
      try {
        const response = await fetch(pokemon.url);
        const data = await response.json();

        const row = document.createElement('tr');
        row.classList.add('pokemon');
        row.innerHTML = `
          <td>${data.name}</td>
          <td><img src="${data.sprites.front_default}" alt="${data.name}"></td>
          <td>#${data.id}</td>
        `;

        row.addEventListener('click', () => {
          modalName.textContent = data.name;
          modalImage.src = data.sprites.front_default;
          modalNumber.textContent = `#${data.id}`;
          modalTypes.textContent = `Types: ${data.types.map(type => type.type.name).join(', ')}`;
          modalAbilities.textContent = `Abilities: ${data.abilities.map(ability => ability.ability.name).join(', ')}`;
          modalHP.textContent = `HP: ${data.stats[0].base_stat}`;
          modalSpeed.textContent = `Speed: ${data.stats[5].base_stat}`;
          modal.style.display = 'block';
        });

        pokedex.appendChild(row);
      } catch (error) {
        console.error('Error fetching Pokemon details:', error);
      }
    };

    closeBtn.addEventListener('click', () => {
      modal.style.display = 'none';
    });

window.addEventListener('click', (event) => {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
});

fetchPokemons();
