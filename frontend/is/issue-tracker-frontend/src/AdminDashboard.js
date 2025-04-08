import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  Container,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Select,
  MenuItem,
  Box,
  CircularProgress,
  Chip,
  Alert,
} from '@mui/material';
import { PriorityHigh } from '@mui/icons-material';

function AdminDashboard({ token }) {
  const [issues, setIssues] = useState([]);
  const [users, setUsers] = useState([]);
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const [issuesResponse, usersResponse, projectsResponse] = await Promise.all([
          axios.get('https://localhost:7271/api/Issue', { headers: { Authorization: `Bearer ${token}` } }),
          axios.get('https://localhost:7271/api/User', { headers: { Authorization: `Bearer ${token}` } }),
          axios.get('https://localhost:7271/api/Project', { headers: { Authorization: `Bearer ${token}` } }),
        ]);
        setIssues(issuesResponse.data || []);
        setUsers(usersResponse.data || []);
        setProjects(projectsResponse.data || []);
        setError(null);
      } catch (error) {
        console.error('Veri çekme hatası:', error.message);
        setError(error.message || 'Veriler yüklenirken hata oluştu.');
      } finally {
        setLoading(false);
      }
    };
    if (token) fetchData();
  }, [token]);

  const handleStatusChange = async (issueId, newStatus) => {
    try {
      const issueToUpdate = issues.find((issue) => issue.id === issueId);
      const updatedIssue = { ...issueToUpdate, status: newStatus };
      await axios.put(`https://localhost:7271/api/Issue/${issueId}`, updatedIssue, {
        headers: { Authorization: `Bearer ${token}` },
      });
      setIssues(issues.map((issue) => (issue.id === issueId ? { ...issue, status: newStatus } : issue)));
      alert('Durum güncellendi!');
    } catch (error) {
      console.error('Durum güncelleme hatası:', error.response ? error.response.data : error.message);
      alert('Durum güncellenirken hata oluştu!');
    }
  };

  const getPriorityColor = (priority) => {
    switch (priority) {
      case 0: return 'success';
      case 1: return 'warning';
      case 2: return 'error';
      case 3: return 'error';
      default: return 'default';
    }
  };

  const getUserNameById = (userId) => {
    const user = users.find((u) => u.id === userId);
    return user ? user.username : 'N/A';
  };

  const getProjectNameById = (projectId) => {
    const project = projects.find((p) => p.id === projectId);
    return project ? project.name : 'N/A';
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" sx={{ mb: 3, textAlign: 'center' }}>
        [Ta]lepler
      </Typography>
      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5 }}>
          <CircularProgress sx={{ color: '#00D4FF' }} />
        </Box>
      ) : error ? (
        <Alert severity="error" sx={{ mb: 2, bgcolor: '#FF6F61', color: '#FFFFFF' }}>
          {error}
        </Alert>
      ) : issues.length > 0 ? (
        <TableContainer component={Paper} sx={{ bgcolor: '#1E2F38', borderRadius: 2, boxShadow: '0 0 15px rgba(0, 212, 255, 0.3)' }}>
          <Table>
            <TableHead>
              <TableRow sx={{ bgcolor: '#1A2C34' }}>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Başlık</TableCell>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Öncelik</TableCell>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Açıklama</TableCell>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Proje</TableCell>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Oluşturan</TableCell>
                <TableCell sx={{ color: '#00D4FF', fontWeight: 'bold' }}>Durum</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {issues.map((issue) => (
                <TableRow key={issue.id} sx={{ '&:hover': { bgcolor: '#2A3F4A' } }}>
                  <TableCell sx={{ color: '#E0E0E0' }}>{issue.title}</TableCell>
                  <TableCell>
                    <Chip
                      icon={<PriorityHigh sx={{ color: '#FFFFFF !important' }} />}
                      label={
                        issue.priority === 0 ? 'Düşük' :
                        issue.priority === 1 ? 'Orta' :
                        issue.priority === 2 ? 'Yüksek' :
                        issue.priority === 3 ? 'Kritik' : 'N/A'
                      }
                      color={getPriorityColor(issue.priority)}
                      sx={{ bgcolor: getPriorityColor(issue.priority), color: '#FFFFFF' }}
                    />
                  </TableCell>
                  <TableCell sx={{ color: '#E0E0E0' }}>{issue.description || '-'}</TableCell>
                  <TableCell sx={{ color: '#E0E0E0' }}>{getProjectNameById(issue.projectId)}</TableCell>
                  <TableCell sx={{ color: '#E0E0E0' }}>{getUserNameById(issue.createdByUserId)}</TableCell>
                  <TableCell>
                    <Select
                      value={issue.status || 0}
                      onChange={(e) => handleStatusChange(issue.id, Number(e.target.value))}
                      sx={{ bgcolor: '#1A2C34', color: '#E0E0E0', borderRadius: 1, '& .MuiSvgIcon-root': { color: '#00D4FF' } }}
                    >
                      <MenuItem value={0}>Açık</MenuItem>
                      <MenuItem value={1}>Devam Ediyor</MenuItem>
                      <MenuItem value={2}>Çözüldü</MenuItem>
                      <MenuItem value={3}>Kapalı</MenuItem>
                    </Select>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      ) : (
        <Alert severity="info" sx={{ mt: 3, bgcolor: '#00D4FF', color: '#1A2C34' }}>
          Henüz talep yok.
        </Alert>
      )}
    </Container>
  );
}

export default AdminDashboard;