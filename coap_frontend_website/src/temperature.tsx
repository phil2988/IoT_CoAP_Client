import { Box, Typography } from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";

type Root = {
  contentType: any;
  serializerSettings: any;
  statusCode: any;
  value: Value[];
};

type Value = {
  id: string;
  timeMeasured: string;
  sensorMeasurements: SensorMeasurement[];
};

type SensorMeasurement = {
  id: string;
  sensorNumber: number;
  temperature: number;
};

const Temperature: FunctionComponent = () => {
  const [values, setValues] = useState<Value[]>([]);

  // const url = "https://localhost:7026/WeatherStation/Newest";
  const url = "https://localhost:7026/WeatherStation/All";

  useEffect(() => {
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((data: Root) => {
        setValues(data.value);
      });
  }, []);

  useEffect(() => {
    console.log(values.length);
    console.log(values);
  }, [values]);

  //   if (values.length > 1)
  return (
    <Box p="5%">
      <Typography variant="h2" textAlign="start" pb="5vh">
        {values.length > 1 ? "All Measurements" : "Measurements"}
      </Typography>
      {values
        ? values.map((m) => {
            return (
              <>
                <Typography textAlign="start" variant="h5" fontWeight="bold">
                  {"Measurement from: " +
                    new Date(m.timeMeasured).toDateString()}
                </Typography>
                {m.sensorMeasurements.map((s) => {
                  return (
                    <Box>
                      <Typography textAlign="start" fontWeight="bold">
                        Sensor id:
                      </Typography>

                      <Typography textAlign="start">{s.id}</Typography>

                      <Typography textAlign="start" fontWeight="bold">
                        Temperature:
                      </Typography>

                      <Typography textAlign="start">{s.temperature}</Typography>
                    </Box>
                  );
                })}
              </>
            );
          })
        : "Loading..."}
    </Box>
  );
};

export default Temperature;
