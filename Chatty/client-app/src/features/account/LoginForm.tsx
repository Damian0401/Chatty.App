import { Button, ButtonGroup, Center, Container, Flex, Spacer, Link as ChakraLink } from "@chakra-ui/react";
import { Formik } from "formik";
import { Link } from "react-router-dom";
import { UserLoginValues } from "../../app/models/user";
import * as Yup from 'yup';
import MyInput from "../../app/common/form/MyInput";
import { useStore } from "../../app/stores/store";

export default function LoginForm() {

    const { userStore } = useStore();

    const initialValues: UserLoginValues = {
        email: '',
        password: '',
    }

    return (
        <Center height='100vh'>
            <Container maxWidth='md' variant='main-style'>
                <Formik
                    initialValues={initialValues}
                    onSubmit={(values => userStore.login(values))}
                    validationSchema={Yup.object({
                        email: Yup.string().required().email(),
                        password: Yup.string().required()
                    })}
                >
                    {({ handleSubmit, errors, touched }) => (
                        <form onSubmit={handleSubmit}>
                            <MyInput
                                label="Email"
                                name="email"
                                errors={errors.email}
                                touched={touched.email}
                                type="email"
                            />
                            <MyInput
                                label="Password"
                                name="password"
                                errors={errors.password}
                                touched={touched.password}
                                type="password"
                            />
                            <Flex pt='2'>
                                <ButtonGroup variant='main-style'>
                                    <Button type='submit'>
                                        Login
                                    </Button>
                                    <Button as={Link} to='/'>
                                        Back
                                    </Button>
                                </ButtonGroup>
                                <Spacer />
                                <ChakraLink as={Link} to='/register' variant='main-style'>
                                    Already have an account?
                                </ChakraLink>
                            </Flex>
                        </form>
                    )}
                </Formik>
            </Container>
        </Center>
    )
}