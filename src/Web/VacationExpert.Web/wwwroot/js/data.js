import * as api from "../api.js"

async function FetchSuggestions(term) {
    return await api.post('/api/search/suggestions', {name:term})
}

async function sendReview(review) {
    return await api.post('/api/review/addReview', review)
}
export {
    FetchSuggestions,
    sendReview,
}