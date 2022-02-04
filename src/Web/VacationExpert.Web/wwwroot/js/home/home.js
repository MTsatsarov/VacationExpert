import * as service from "../data.js"
import { html, render } from "../../lib/node_modules/lit-html/lit-html.js"
var input = document.getElementsByName('location-input')[0];
input.addEventListener('change', getSuggestions)
var article = document.getElementById('suggestionBox');

async function getSuggestions() {
    var cities = await service.FetchSuggestions(input.value);
console.log(cities);
    render(suggestionsTemplate(cities, setValue), article)


    function setValue(e) {
        var target = e.target;
        if (target.tagName == "P") {
            input.value = target.textContent
          article.innerHTML=''
        }
    }
}
const suggestionsTemplate = (cities, setValue) => html`
    ${cities.map(x => singleCityTemplate(x,setValue))}

`

const singleCityTemplate = (city,clickHandler) => html`
<p @click = ${clickHandler}>${city}</p>`