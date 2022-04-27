import { render, html } from "../../lib/node_modules/lit-html/lit-html.js"
document.getElementsByClassName('create')[0].addEventListener('click', markArticle)
var nav = document.getElementsByClassName('create-nav')[0];
var input = document.getElementById("photo-uploader")
input.addEventListener("change", readURL)
Array.from(document.getElementsByClassName('create-nav')).forEach(x => x.addEventListener('click', changeSection))
var sections = Array.from(document.querySelectorAll("form>section"));

var navLiItemsArray = Array.from(document.querySelectorAll('li[data-section]'));


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