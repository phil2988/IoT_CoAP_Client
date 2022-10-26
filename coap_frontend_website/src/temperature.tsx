import { useState } from "react";

const Temperature = () => {
    const [temperature, setTemperature] = useState<number>();

    const url = "https://localhost:7026/WeatherStation"
    fetch(url).then(response => {
      return response.json()
    }).then(data => {
        setTemperature(data.value.temperature)
    })

    return(
        <div>
            {temperature? <h1>Temperature is {temperature}</h1> : <h1>Loading Temperature...</h1>} 
        </div>
    )    
}
 
export default Temperature;