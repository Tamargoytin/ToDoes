import axios from 'axios';

// const apiUrl = "http://localhost:5192"
axios.defaults.baseURL = "http://localhost:5192";

axios.interceptors.response.use(
  response => response,
  error => {
    console.error('Axios response error:', error);
    return Promise.reject(error);
  }
);

export default {
  getTasks: async () => {
    const result = await axios.get("/items")    
    return result.data;
  },

  addTask: async(name)=>{
      try {
        const result = await axios.post(`/items`, {
          "name": name,
          "isComplete": false 
        });
  
        console.log('addTask response:', result.data);
  
        return result.data;
      } catch (error) {
        console.error('addTask error:', error);
      }
},

setCompleted: async (id, isComplete) => {
  try {
    const todo = await (await axios.get(`/items/${id}`)).data;
    console.log(todo);
    console.log(isComplete);
    // Making a PUT or PATCH request to update the task's completion status]
    const result = await axios.put(`/items/${id}`, {
      "name":todo.name,
      "isComplete": isComplete
    });

    console.log('setCompleted response:', result.data);

    return result.data;
  } catch (error) {
    console.error('setCompleted error:', error);
    throw error; // Propagate the error to the calling function
  }
},


  deleteTask:async(id)=>{
    try {
      const result = await axios.delete(`/items/${id}`);
      console.log('deleteTask response:', result.data);
      return result.data;
    } catch (error) {
      console.error('deleteTask error:', error);
    }
}
};
