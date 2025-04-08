import React, { useState, useEffect } from 'react';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline, AppBar, Toolbar, Typography, Box, Fade, IconButton, Button } from '@mui/material';
import { ExitToApp as LogoutIcon, Add as AddIcon, List as ListIcon } from '@mui/icons-material';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './Login';
import UserDashboard from './UserDashboard';
import UserIssues from './UserIssues';
import AdminLayout from './AdminLayout';
import AdminDashboard from './AdminDashboard';
import AddProject from './AddProject';

const theme = createTheme({
  palette: {
    primary: { main: '#1A2C34' }, // Daha derin mavi
    secondary: { main: '#00D4FF' }, // Parlak neon cyan
    background: { default: '#0F1C22', paper: '#1E2F38' }, // Koyu ama sofistike arkaplan
    text: { primary: '#E0E0E0', secondary: '#00D4FF' }, // Beyaz ve neon vurgu
  },
  typography: {
    fontFamily: "'Orbitron', sans-serif", // Breaking Bad’e uygun fütüristik font
    h4: { fontWeight: 700, color: '#00D4FF', letterSpacing: '1px' },
    h6: { fontWeight: 600, color: '#E0E0E0' },
    button: { textTransform: 'uppercase', fontWeight: 600 },
  },
  components: {
    MuiAppBar: {
      styleOverrides: {
        root: {
          background: 'linear-gradient(90deg, #1A2C34 0%, #0F1C22 100%)',
          boxShadow: '0 4px 20px rgba(0, 212, 255, 0.3)',
          borderBottom: '1px solid #00D4FF',
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          padding: '8px 16px',
          backgroundColor: '#00D4FF',
          color: '#1A2C34',
          boxShadow: '0 2px 10px rgba(0, 212, 255, 0.5)',
          '&:hover': { backgroundColor: '#E0E0E0', color: '#1A2C34' },
        },
      },
    },
  },
});

function App() {
  const [token, setToken] = useState(localStorage.getItem('token') || null);
  const [role, setRole] = useState(localStorage.getItem('role') || null);
  const [showForm, setShowForm] = useState(false);
  const [showIssues, setShowIssues] = useState(false);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    const storedRole = localStorage.getItem('role');
    if (storedToken && storedRole) {
      setToken(storedToken);
      setRole(storedRole);
    } else if (storedToken && !storedRole) {
      handleLogout();
    }
  }, []);

  const handleSetToken = (newToken, newRole) => {
    const roleString = newRole.toString();
    setToken(newToken);
    setRole(roleString);
    localStorage.setItem('token', newToken);
    localStorage.setItem('role', roleString);
    console.log('Yeni role:', roleString);
  };

  const handleLogout = () => {
    setToken(null);
    setRole(null);
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    alert('Oturum kapatıldı!');
  };

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
          <AppBar position="static">
            <Toolbar>
              <Typography variant="h6" component="div" sx={{ flexGrow: 1, fontWeight: 'bold' }}>
                [He][Br] Tracker
              </Typography>
              {token && role !== '0' && (
                <>
                  <Button startIcon={<AddIcon />} onClick={() => setShowForm(!showForm)} sx={{ mr: 2 }}>
                    {showForm ? '[X]' : '[+]'}
                  </Button>
                  <Button startIcon={<ListIcon />} onClick={() => setShowIssues(!showIssues)} sx={{ mr: 2 }}>
                    {showIssues ? '[X]' : '[>]'}
                  </Button>
                </>
              )}
              {token && (
                <IconButton color="inherit" onClick={handleLogout}>
                  <LogoutIcon />
                </IconButton>
              )}
            </Toolbar>
          </AppBar>
          <Fade in={true} timeout={500}>
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
              {!token ? (
                <Login setToken={handleSetToken} />
              ) : role === '0' ? (
                <Routes>
                  <Route path="/" element={<AdminLayout />}>
                    <Route path="admin/dashboard" element={<AdminDashboard token={token} />} />
                    <Route path="admin/add-project" element={<AddProject token={token} />} />
                    <Route index element={<AdminDashboard token={token} />} />
                  </Route>
                </Routes>
              ) : (
                <>
                  <UserDashboard token={token} showForm={showForm} setShowForm={setShowForm} />
                  {showIssues && <UserIssues token={token} />}
                </>
              )}
            </Box>
          </Fade>
          <Box
            component="footer"
            sx={{
              py: 2,
              textAlign: 'center',
              background: 'linear-gradient(90deg, #1A2C34 0%, #0F1C22 100%)',
              color: 'secondary.main',
              borderTop: '1px solid #00D4FF',
            }}
          >
            <Typography variant="body2">
              © {new Date().getFullYear()} [He][Br] Tracker
            </Typography>
          </Box>
        </Box>
      </Router>
    </ThemeProvider>
  );
}

export default App;