import * as api from "../api.js"

async function FetchSuggestions(term) {
    return await api.post('/api/search/suggestions', {name:term})
}

export {
    FetchSuggestions,
}