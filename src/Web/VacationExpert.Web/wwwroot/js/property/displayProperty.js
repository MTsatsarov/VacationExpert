import{html,render} from "../../lib/node_modules/lit-html/lit-html.js";
import * as service from "../data.js"
var reviewBox = document.getElementsByClassName('create-review-box')[0];
reviewBox.addEventListener('click', sendReview)
var reviewContainer = document.getElementById('reviews')
reviewContainer.addEventListener('click', getNewReviews);
var container = document.getElementById('display-container');


document.getElementById("show-review-btn").addEventListener("click", showHideReview)

function showHideReview() {

    if (reviewBox.style.display == 'none' || reviewBox.style.display == "") {
        reviewBox.style.display = 'flex';
    }
    else {
        reviewBox.style.display = 'none';
    }
}

async function sendReview(e) {
    if (e.target.tagName == 'BUTTON') {
        var review = reviewBox.querySelector('textarea');
        await service.sendReview({ content: review.value, propertyId: container.getAttribute('data-property'), rating: reviewBox.querySelector('input').value })

        review.value = '';
        showHideReview();
    }
}


doSmth()
async function doSmth() {
    var result = await service.getAnotherReviews({propertyId:container.getAttribute('data-property'), page: "1"})
    if (result.status == 400) {
    } else {
         reviewContainer = document.querySelector('.property-view-reviews');
        render(containerTemplate(result),container)

    }
}

async function getNewReviews(e) {
    if (e.target.tagName == 'BUTTON') {
       var result = await service.getAnotherReviews({propertyId:container.getAttribute('data-property'), page: e.target.getAttribute('data-page')})
      if (result.status == 400) {
      } else {
           reviewContainer = document.querySelector('.property-view-reviews');
          render(containerTemplate(result),container)

      }
    }
}


const containerTemplate = (reviews) =>html `
 <article ${onclick=getNewReviews} class="property-view-reviews">
 <button data-page=${Number(reviews.currentPage) -1} class="review-nav-btn">Prev</button>
    ${reviews.reviews.map(x=>singleTemplate(x))}
        <button data-page=${Number(reviews.currentPage) +1}  class="review-nav-btn">Next</button>
        </article>
`

const singleTemplate = (a) => html `
        <div class="single-review">
                <div class="single-review-header">
                    <p>${a.username}</p> <p>${a.dateTime}</p> <p class='review-rating'>${a.rating}</p>
                </div>
                <p class='review-content'> ${a.content}</p>
            </div>`