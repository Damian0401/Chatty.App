import { Route, Routes } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import LoginForm from "../../features/account/LoginForm";
import RegisterForm from "../../features/account/RegisterForm";
import NotFound from "../../features/errors/NotFound";
import HomePage from "../../features/home/HomePage";
import ToggleThemeButton from "./ToggleThemeButton";

function App() {

  return (
    <>
      <ToastContainer position='bottom-right' hideProgressBar />
      <ToggleThemeButton />
      <Routes>
        <Route index element={<HomePage />} />
        <Route path='login' element={<LoginForm />} />
        <Route path='register' element={<RegisterForm />} />
        <Route path='*' element={<NotFound />} />
      </Routes>
    </>
  );
}

export default App;
