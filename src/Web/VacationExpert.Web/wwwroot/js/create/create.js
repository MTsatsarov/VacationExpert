import { render, html } from "../../lib/node_modules/lit-html/lit-html.js"
document.getElementsByClassName('create')[0].addEventListener('click', markArticle)
var nav = document.getElementsByClassName('create-nav')[0];
document.getElementById('addBed').addEventListener('click', addBed)
var input = document.getElementById("photo-uploader")
input.addEventListener("change", readURL)
document.getElementById("addRoom").addEventListener('click',AddRoom)
Array.from(document.getElementsByClassName('create-nav')).forEach(x => x.addEventListener('click', changeSection))
var sections = Array.from(document.querySelectorAll("form>section"));

var navLiItemsArray = Array.from(document.querySelectorAll('li[data-section]'));
function AddRoom(e) {
  var section = document.getElementsByClassName('bed-options-wrapper')[0]
    section.innerHTML +=  ` <article class="type-smoking" asp-for="Rooms">
    <label asp-for="Rooms[i].Type">Room type</label>
    <select asp-for="Rooms[i].Type" asp-items="@Html.GetEnumSelectList<RoomType>()"></select>

    <label asp-for="Rooms[i].SmokingPolicy">Smoking policy</label>
    <select asp-for="Rooms[i].SmokingPolicy" asp-items="@Html.GetEnumSelectList<SmokingPolicy>()"></select>

    <label asp-for="Rooms[i].RoomCount">Number of rooms (this type)</label>
    <input min="1" asp-for="Rooms[i].RoomCount" type="number" /> `
};

function markArticle(e) {
    var articles = document.getElementsByTagName('article');
    Array.from(articles).forEach(x => x.classList.remove('clicked-article'))
    var currentNode = e.target;
    if (currentNode.tagName == 'ARTICLE') {
        currentNode.classList.add('clicked-article')
      

    } else {
        while (currentNode.tagName != "SECTION") {
            if (currentNode.tagName == 'ARTICLE') {
                currentNode.classList.add('clicked-article')
                console.log("inside")
                break;
            }
            else {
                currentNode = currentNode.parentNode
            }
        }
    }
}
init();
function init() {
    var a = Array.from(document.querySelectorAll("li[data-section = basic-info]")).forEach(x => x.classList.add('clicked-part'))
    sections.forEach(x => x.style.display = "none")
    document.querySelector(".basic-info").style.display = "flex";

}
function changeSection(e) {
    if (e.target.tagName == 'LI') {
        var data = e.target.getAttribute('data-section')
        navLiItemsArray.forEach(x => x.classList.remove('clicked-part'))

        navLiItemsArray.filter(x => x.getAttribute('data-section') == data).forEach(x => x.classList.add('clicked-part'))

        sections.forEach(x => x.style.display = "none")
        var currentSelected = sections.find(x => x.classList.contains(data)).style.display = 'flex';
    }

}

function readURL() {
    console.log(input.files);

    for ( var ai of input.files) {
        let reader = new FileReader();
        reader.onload = () => {
            let img = document.createElement('img');
            img.setAttribute('src', reader.result)
            var div = document.createElement('div');
            div.appendChild(img);
            
            var article = document.getElementById('uploaded-photo-gallery');
            article.appendChild(div);
        }

        reader.readAsDataURL(ai);
    }
}


const listTemplate = (images) => html`
    <article id="uploaded-photo-gallery">
    
    </article>
    `
