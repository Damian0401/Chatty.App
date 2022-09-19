import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Route, Routes } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { history } from "../..";
import LoginForm from "../../features/account/LoginForm";
import RegisterForm from "../../features/account/RegisterForm";
import ChatPanel from "../../features/chat/panel/ChatPanel";
import ChatRoom from "../../features/chat/room/ChatRoom";
import NotFound from "../../features/errors/NotFound";
import HomePage from "../../features/home/HomePage";
import ModalContainer from "../common/modals/ModalContainer";
import { useStore } from "../stores/store";
import Dashboard from "./Dashboard";
import ToggleThemeButton from "./ToggleThemeButton";

function App() {

  const { userStore, commonStore } = useStore();

  useEffect(() => {
    if (!commonStore.token) {
      history.push('/');
      return;
    };
    history.push('/chat')
    userStore.getUser();
  }, []);

  return (
    <>
      <ToastContainer position='bottom-right' hideProgressBar />
      <ModalContainer />
      <ToggleThemeButton />
      <Routes>
        <Route index element={<HomePage />} />
        <Route path='login' element={<LoginForm />} />
        <Route path='register' element={<RegisterForm />} />
        <Route path='chat' element={<Dashboard />}>
          <Route index element={<ChatPanel />} />
          <Route path=':id' element={<ChatRoom />} />
          <Route path='notfound' element={<NotFound content={<ChatPanel />} />} />
        </Route>
        <Route path='*' element={<NotFound />} />
      </Routes>
    </>
  );
}

export default observer(App);
