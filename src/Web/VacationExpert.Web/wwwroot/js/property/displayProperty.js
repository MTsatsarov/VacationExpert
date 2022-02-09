import * as service from "../data.js"
var reviewBox = document.getElementsByClassName('create-review-box')[0];
reviewBox.addEventListener('click',sendReview)
document.getElementById("show-review-btn").addEventListener("click", showHideReview)

function showHideReview() {

    if (reviewBox.style.display == 'none' || reviewBox.style.display=="") {
        reviewBox.style.display = 'flex';
    }
    else {
        reviewBox.style.display = 'none';
    }
}

async function sendReview(e) {
    if (e.target.tagName == 'BUTTON') {
        var review = reviewBox.querySelector('textarea');

        await service.sendReview({content:review.value, propertyId: review.getAttribute('data-property'),rating:reviewBox.querySelector('input').value})

        review.value='';
        showHideReview();
    }
}