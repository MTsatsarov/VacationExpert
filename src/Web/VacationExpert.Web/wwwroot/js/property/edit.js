import * as api from "../../api.js"

const removeImage = async (e) => {
        var target = e.target;
        if(target.tagName == 'I')
        {
                e.preventDefault();
                target  = target.parentNode
        }
        if (target.tagName == "BUTTON") {
                e.preventDefault();
                if(confirm('Are you sure, you want to delete this image ?')) {
                        var id = target.getAttribute('data-id');
                        var response = await api.post(api.host + '/image/delete', id)
                        if (Number(response.status) >= 300) {
                                console.log('invalid');
                        }
                        else {
                                target.parentNode.remove();
        
                        }
                }
                
        }

}

document.getElementById('edit-image-container').addEventListener('click', removeImage)

