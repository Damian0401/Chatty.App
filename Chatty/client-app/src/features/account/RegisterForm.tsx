import { Button, ButtonGroup, Center, Container, Flex, Spacer, Link as ChakraLink } from "@chakra-ui/react";
import { Formik } from "formik";
import { Link } from "react-router-dom";
import { UserRegisterValues } from "../../app/models/user";
import * as Yup from 'yup';
import MyInput from "../../app/common/form/MyInput";
import { useStore } from "../../app/stores/store";


export default function RegisterForm() {

    const { userStore } = useStore();

    const initialValues: UserRegisterValues = {
        email: '',
        password: '',
        userName: ''
    }

    return (
        <Center height='100vh'>
            <Container maxWidth='md' variant='main-style'>
                <Formik
                    initialValues={initialValues}
                    onSubmit={(values => userStore.register(values))}
                    validationSchema={Yup.object({
                        email: Yup.string().required().email(),
                        password: Yup.string().required().min(6),
                        userName: Yup.string().required()
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
                            <MyInput
                                label="UserName"
                                name="userName"
                                errors={errors.userName}
                                touched={touched.userName}
                                type="text"
                            />
                            <Flex pt='2'>
                                <ButtonGroup variant='main-style'>
                                    <Button type='submit'>
                                        Submit
                                    </Button>
                                    <Button as={Link} to='/'>
                                        Back
                                    </Button>
                                </ButtonGroup>
                                <Spacer />
                                <ChakraLink as={Link} to='/login' variant='main-style'>
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