import React, { useState } from 'react';
import axios from 'axios';
import {
  Card,
  CardContent,
  Box,
  TextField,
  Button,
  Typography,
  Alert,
  CircularProgress,
  InputAdornment,
  IconButton,
  Fade,
} from '@mui/material';
import { Visibility, VisibilityOff, Lock as LockIcon, Person as PersonIcon, ErrorOutline } from '@mui/icons-material';
import { styled, ThemeProvider, createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: { main: '#14202B' }, // Daha koyu ve gizemli mavi
    secondary: { main: '#00E5FF' }, // Daha parlak neon cyan
    background: { default: '#0A1419', paper: '#1C2A35' }, // Derin laboratuvar havası
    text: { primary: '#F0F0F0', secondary: '#00E5FF' }, // Temiz beyaz ve neon
  },
  typography: {
    fontFamily: "'Orbitron', sans-serif", // Fütüristik Heisenberg fontu
    h5: { fontWeight: 800, color: '#00E5FF', letterSpacing: '2px' },
    body2: { fontWeight: 500, color: '#F0F0F0' },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 10,
          textTransform: 'uppercase',
          fontWeight: 700,
          padding: '12px 24px',
          background: 'linear-gradient(135deg, #00E5FF 0%, #14202B 80%)',
          color: '#14202B',
          boxShadow: '0 5px 20px rgba(0, 229, 255, 0.6), inset 0 0 10px rgba(0, 229, 255, 0.3)',
          transition: 'all 0.4s ease',
          '&:hover': {
            background: 'linear-gradient(135deg, #FFFFFF 0%, #00E5FF 80%)',
            boxShadow: '0 8px 25px rgba(0, 229, 255, 0.8), inset 0 0 15px rgba(255, 255, 255, 0.4)',
            transform: 'scale(1.05)',
          },
          '&:disabled': {
            background: 'rgba(0, 229, 255, 0.2)',
            boxShadow: 'none',
            color: '#14202B',
          },
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: 10,
            backgroundColor: '#1C2A35',
            border: '2px solid #00E5FF',
            color: '#F0F0F0',
            fontWeight: 500,
            transition: 'all 0.4s ease',
            '&:hover': {
              borderColor: '#FFFFFF',
              backgroundColor: '#253541',
              boxShadow: '0 0 15px rgba(0, 229, 255, 0.3)',
            },
            '&.Mui-focused': {
              borderColor: '#00E5FF',
              boxShadow: '0 0 20px rgba(0, 229, 255, 0.6)',
            },
            '&.Mui-error': {
              borderColor: '#FF4D4D',
              boxShadow: '0 0 20px rgba(255, 77, 77, 0.5)',
            },
            '& .MuiOutlinedInput-notchedOutline': {
              border: 'none',
            },
          },
          '& .MuiInputLabel-root': {
            color: '#00E5FF',
            fontWeight: 600,
            '&.Mui-focused': {
              color: '#FFFFFF',
            },
            '&.Mui-error': {
              color: '#FF4D4D',
            },
          },
        },
      },
    },
    MuiAlert: {
      styleOverrides: {
        root: {
          borderRadius: 10,
          padding: '10px 20px',
          backgroundColor: 'rgba(255, 77, 77, 0.15)',
          border: '1px solid #FF4D4D',
          color: '#F0F0F0',
          fontWeight: 500,
          boxShadow: '0 0 10px rgba(255, 77, 77, 0.3)',
          transition: 'opacity 0.4s ease',
          display: 'flex',
          alignItems: 'center',
          gap: 1,
        },
      },
    },
  },
});

const StyledCard = styled(Card)(({ theme }) => ({
  minWidth: 360,
  maxWidth: 450,
  background: 'linear-gradient(145deg, #1C2A35 0%, #0A1419 100%)',
  boxShadow: '0 15px 40px rgba(0, 229, 255, 0.25), 0 5px 20px rgba(0, 229, 255, 0.15)',
  borderRadius: 20,
  border: '2px solid #00E5FF',
  overflow: 'hidden',
  animation: 'glowPulse 2s infinite alternate, fadeInUp 0.8s ease-out forwards',
  '&:hover': {
    boxShadow: '0 20px 50px rgba(0, 229, 255, 0.4), 0 8px 25px rgba(0, 229, 255, 0.2)',
    borderColor: '#FFFFFF',
  },
  '@keyframes glowPulse': {
    '0%': { boxShadow: '0 15px 40px rgba(0, 229, 255, 0.25), 0 5px 20px rgba(0, 229, 255, 0.15)' },
    '100%': { boxShadow: '0 15px 40px rgba(0, 229, 255, 0.4), 0 5px 20px rgba(0, 229, 255, 0.25)' },
  },
  '@keyframes fadeInUp': {
    '0%': { opacity: 0, transform: 'translateY(30px)' },
    '100%': { opacity: 1, transform: 'translateY(0)' },
  },
}));

function Login({ setToken }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const handleLogin = async () => {
    setLoading(true);
    setError(null);
    try {
      console.log('Giriş isteği:', { username, password });
      const response = await axios.post('https://localhost:7271/api/Auth/login', {
        username,
        password,
      });
      console.log('Backend yanıtı:', response.data);
      const { token, role } = response.data;
      if (!token) {
        throw new Error('Token alınamadı!');
      }
      const userRole = role !== undefined ? role : '1';
      setToken(token, userRole);
      localStorage.setItem('token', token);
      localStorage.setItem('role', userRole);
      alert('Giriş başarılı!');
    } catch (error) {
      const errorMessage = error.response
        ? `Hata: ${error.response.data.message || 'Giriş başarısız.'}`
        : 'Sunucuya ulaşılamadı. Backend açık mı dayı?';
      console.error('Giriş hatası:', errorMessage);
      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  return (
    <ThemeProvider theme={theme}>
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          minHeight: '100vh',
          background: 'linear-gradient(145deg, #0A1419 0%, #14202B 100%)', // Daha havalı gradient
        }}
      >
        <Fade in={true} timeout={1000}>
          <StyledCard>
            <CardContent sx={{ p: 6 }}>
              <Fade in={true} timeout={1200}>
                <Typography variant="h5" align="center" gutterBottom>
                  [He][Br] Giriş
                </Typography>
              </Fade>
              <Fade in={true} timeout={1400}>
                <Typography variant="body2" align="center" sx={{ mb: 4 }}>
                  Laboratuvara Hoş Geldin Dayı
                </Typography>
              </Fade>
              <Fade in={true} timeout={1600}>
                <TextField
                  label="[Ku]llanıcı Adı"
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  autoFocus
                  disabled={loading}
                  error={!!error}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <PersonIcon sx={{ color: error ? '#FF4D4D' : '#00E5FF' }} />
                      </InputAdornment>
                    ),
                  }}
                  sx={{ mb: 3 }}
                />
              </Fade>
              <Fade in={true} timeout={1800}>
                <TextField
                  label="[Şi]fre"
                  type={showPassword ? 'text' : 'password'}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  disabled={loading}
                  error={!!error}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <LockIcon sx={{ color: error ? '#FF4D4D' : '#00E5FF' }} />
                      </InputAdornment>
                    ),
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton
                          onClick={() => setShowPassword(!showPassword)}
                          edge="end"
                          sx={{ color: '#00E5FF' }}
                          disabled={loading}
                        >
                          {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                      </InputAdornment>
                    ),
                  }}
                  sx={{ mb: 3 }}
                />
              </Fade>
              {error && (
                <Fade in={true} timeout={2000}>
                  <Alert severity="error" sx={{ mb: 3 }}>
                    <ErrorOutline sx={{ mr: 1 }} />
                    {error}
                  </Alert>
                </Fade>
              )}
              <Fade in={true} timeout={2200}>
                <Button
                  variant="contained"
                  fullWidth
                  onClick={handleLogin}
                  disabled={loading || !username || !password}
                  sx={{ mt: 1 }}
                >
                  {loading ? (
                    <CircularProgress size={24} sx={{ color: '#14202B' }} />
                  ) : (
                    '[+] Giriş Yap'
                  )}
                </Button>
              </Fade>
              <Fade in={true} timeout={2400}>
                <Typography variant="body2" align="center" sx={{ mt: 3 }}>
                  Şifren mi uçtu?{' '}
                  <Box component="span" sx={{ color: '#00E5FF', cursor: 'pointer', '&:hover': { color: '#FFFFFF' } }}>
                    [Ye]nile
                  </Box>
                </Typography>
              </Fade>
            </CardContent>
          </StyledCard>
        </Fade>
      </Box>
    </ThemeProvider>
  );
}

export default Login;