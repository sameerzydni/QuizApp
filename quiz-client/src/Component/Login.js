import { Card, CardContent, TextField, Typography, width } from "@mui/material";
import { Button } from "@mui/material";
import { Box } from "@mui/material";
import React, {useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { createAPIEndPoint, ENDPOINT } from "../api";
import useForm from "../hooks/useForm";
import useStateContext from "../hooks/useStateContext";
import Center from "./Center";

const getFreshModel = () => ({
    name: '',
    email: ''
})

export default function Login() {

    const {context, setContext, resetContext} = useStateContext();
    const navigate = useNavigate();

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getFreshModel);

    useEffect(() => {
        resetContext()
    }, [])

    
    const login = e => {
        e.preventDefault();
        if(validate())
            createAPIEndPoint(ENDPOINT.participant)
            .post(values)
            .then(res => {
                setContext({participantId:res.data.participantId})
                // console.log(context);
                navigate('/quiz')
            })
            .catch(err => console.log(err))
    }

    const validate = () => {
        let temp = {}
        temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not validate."
        temp.name = values.name != "" ? "" : "The field is required."
        setErrors(temp)
        return Object.values(temp).every(x => x == "")
    }

    return (
        <Center>
            <Card sx={{ width: 400 }}>
                <CardContent sx={{ textAlign: 'center' }}>
                    <Typography variant="h3" sx={{ my: 3 }}>
                        Quiz App
                    </Typography>
                    <Box sx={{
                        '& 	.MuiTextField-root': {
                            m: 1,
                            width: '90%'
                        }
                    }}>
                        <form noValidate autoComplete="off" onSubmit={login}>
                            <TextField 
                                label="Email" 
                                name="email" 
                                variant="outlined" 
                                value={values.email} 
                                onChange={handleInputChange} 
                                {...(errors.email && {error:true, helperText:errors.email})}/>
                            <TextField 
                                label="Name" 
                                name="name" 
                                variant="outlined" 
                                value={values.name} 
                                onChange={handleInputChange} 
                                {...(errors.name && {error:true, helperText:errors.name})}/>
                            <Button type="submit" size="large" variant="contained" sx={{ width: '90%' }}>Start</Button>
                        </form>
                    </Box>
                </CardContent>
            </Card>
        </Center>


    )
}