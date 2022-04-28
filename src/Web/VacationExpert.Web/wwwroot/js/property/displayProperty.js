import { html, render } from "../../lib/node_modules/lit-html/lit-html.js";
import * as service from "../data.js"

var reviewBox = document.getElementsByClassName('create-review-box')[0];
reviewBox.addEventListener('click', sendReview)

var reviewContainer = document.getElementById('reviews')

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
    console.log('doSmth');
    var result = await service.getAnotherReviews({ propertyId: container.getAttribute('data-property'), page: "1" })
    if (result.status == 400) {
    } else {
        reviewContainer = document.querySelector('.property-view-reviews');
        render(containerTemplate(result,vote), container)

    }
}

async function getNewReviews(e) {
    if (e.target.tagName == 'BUTTON') {
        var result = await service.getAnotherReviews({ propertyId: container.getAttribute('data-property'), page: e.target.getAttribute('data-page') })
        if (result.status == 400) {
        } else {
            reviewContainer = document.querySelector('.property-view-reviews');
            render(containerTemplate(result,vote), container)
        }
    }
}


const containerTemplate = (reviews,vote) => html`
<article  class="property-view-reviews">
    <button ${onclick = getNewReviews} data-page=${Number(reviews.currentPage) - 1} class="review-nav-btn">Prev</button>
    ${reviews.reviews.map(x => singleTemplate(x,vote))}
    <button ${onclick = getNewReviews} data-page=${Number(reviews.currentPage) + 1} class="review-nav-btn">Next</button>
</article>
`
async function vote(e) {
if(e.target.tagName=='BUTTON')
var reviewId = e.target.getAttribute("data-id");
var like = e.target.getAttribute('data-like') == 'true' ? true :false
await service.Vote({reviewId,like})

doSmth()

}

const singleTemplate = (review,vote) => html`
        <div class="single-review">
            <div class="single-review-header">
                <p>${review.username}</p>
                <p>${review.dateTime}</p>
                <p class='review-rating'>${review.rating}</p>
            </div>
            <p class='review-content'> ${review.content}</p>
            <div class="single-review-header">
                <div @click=${vote}>
                <button class="like-review" data-id=${review.id} data-like=${true} >${review.likeCount}</button>
                    <button class="dislike-review" data-id=${review.id}  data-like=${false}>${review.dislikeCount}</button>
                </div>
            </div>
        </div>`