import { CalendarIcon, ChatIcon, CloseIcon, HamburgerIcon } from "@chakra-ui/icons";
import { Flex, HStack, Icon, IconButton, Menu, MenuButton, MenuItem, MenuList, Spacer, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { useStore } from "../stores/store";



export default observer(function Navbar() {

    const { userStore } = useStore();

    return (
        <>
            <Flex p='3' bgColor='blackAlpha.700' fontFamily='fantasy'>
                <HStack as={Link} to='/chat' color='whiteAlpha.800'>
                    <Icon as={CalendarIcon} w='8' h='8' />
                    <Text fontSize='3xl' noOfLines={1}>
                        Chattty App
                    </Text>
                </HStack>
                <Spacer />
                <HStack>
                    <Text color='whiteAlpha.800'>
                        {userStore.user?.userName || '{userName}'}
                    </Text>
                    <Menu>
                        <MenuButton
                            as={IconButton}
                            aria-label='Options'
                            icon={<HamburgerIcon />}
                            variant='main-style'
                            boxShadow='base'
                            color='whiteAlpha.800'
                        />
                        <MenuList>
                            <MenuItem icon={<ChatIcon />} as={Link} to='/chat'>
                                Chat panel
                            </MenuItem>
                            <MenuItem icon={<CloseIcon />} onClick={() => userStore.logout()}>
                                Logout
                            </MenuItem>
                        </MenuList>
                    </Menu>
                </HStack>
            </Flex>
        </>
    )
})