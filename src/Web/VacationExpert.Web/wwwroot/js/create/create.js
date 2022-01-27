document.getElementsByClassName('create')[0].addEventListener('click', markArticle)
var nav = document.getElementsByClassName('create-nav')[0];
document.getElementById('addBed').addEventListener('click',addBed)

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
    Array.from(document.querySelectorAll("li[data-section = basic-info]")).forEach(x => x.classList.add('clicked-part'))
    sections.forEach(x => x.style.display = "none")
    document.querySelector(".basic-info").style.display = "flex";

}
function addBed () {

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


