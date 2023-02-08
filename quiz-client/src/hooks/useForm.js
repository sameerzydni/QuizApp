import React, { useState } from "react";

export default function useForm(getFreshmodelObject){
    const[values, setValues] = useState(getFreshmodelObject());
    const[errors, setErrors] = useState({});

    const handleInputChange = e => {
        const {name, value} = e.target
        setValues({
            ...values,
            [name]: value
        })
    }

    return{
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    }
}