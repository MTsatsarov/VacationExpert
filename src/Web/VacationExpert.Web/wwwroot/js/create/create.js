document.getElementsByClassName('create')[0].addEventListener('click', markArticle)
var nav = Array.from(document.getElementsByClassName('create-nav')).forEach(x=>x.addEventListener('click', changeSection))

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


function changeSection(e) {
    if (e.target.tagName == 'LI') {
        var data = e.target.getAttribute('data-section')
        Array.from(document.querySelectorAll('li[data-section]')).forEach(x => x.classList.remove('clicked-part'))
        Array.from(document.querySelectorAll('li[data-section]')).filter(x=>x.getAttribute('data-section') == data).forEach(x=>x.classList.add('clicked-part'))

        var sections = Array.from(document.querySelectorAll("form>section"));
        sections.forEach(x => x.style.display = "none")
       var currentSelected = sections.find(x=>x.classList.contains(data));
       currentSelected.style.display= 'flex';
    }

}