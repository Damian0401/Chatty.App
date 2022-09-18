import { observer } from "mobx-react-lite";
import { Message } from "../../../app/models/message";
import ChatMessage from "./ChatMessage";
import ChatMessageDeleted from "./ChatMessageDeleted";
import ChatMessageSystem from "./ChatMessageSystem";

interface Props {
    message: Message;
}

export default observer(function ChatMessageContainer({ message }: Props) {

    if (!message.authorId) return <ChatMessageSystem message={message} />;

    if (message.isDeleted) return <ChatMessageDeleted message={message} />;

    return <ChatMessage message={message} />;
})