import { Button, Center } from "@chakra-ui/react";
import { Link } from "react-router-dom";


export default function HomePage() {

    return (
        <Center height='100vh'>
            <Button 
                m='2' size='md' 
                variant='main-style'
                as={Link}
                to='login'    
            >
                Login
            </Button>
            <Button 
                m='2' size='md' 
                variant='main-style'
                as={Link}
                to='register' 
            >
                Register
            </Button>
        </Center>
    )
}