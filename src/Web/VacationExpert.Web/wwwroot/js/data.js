import * as api from "../api.js"

async function FetchSuggestions(term) {
    return await api.post('/api/search/suggestions', {name:term})
}

async function sendReview(review) {
    return await api.post('/api/review/addReview', review)
}

async function Vote(vote) {
    return await api.post('/api/vote/Vote', vote)
}

async function getAnotherReviews(reviews) {
    return await api.post('/api/review/GetReviews', reviews)
}
export {
    FetchSuggestions,
    sendReview,
    getAnotherReviews,
    Vote,
}